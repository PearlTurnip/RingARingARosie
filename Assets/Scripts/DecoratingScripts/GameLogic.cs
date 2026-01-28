using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    // Collecting UI
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] GameObject[] flowerPots;

    private bool running = true;

    private float timeRemaining = 120f;
    private int score = 0;
    private bool flowerPotsFilled = false;

    [SerializeField] private GameObject currentMask;


    public void TogglePlay() {
        running = !running;
    }

    public void SetPlay(bool play) {
        running = play;
    }

    public void Reset() {
        timeRemaining = 120f;
    }

    public void SetOrder(Order newOrder) {
        currentMask.GetComponent<Mask>().SetCurrentOrder(newOrder);
    }

    private string ConvertToTime() {
        // Doubt we're gonna need anything higher than a minute right?
        int min = (int)(timeRemaining / 60f);
        int sec = (int)(timeRemaining % 60f);

        if (timeRemaining <= 0) return "0:00";
        if (sec < 10) return $"{min}:0{sec}";
        return $"{min}:{sec}";
    }

    private void UIUpdate() {
        scoreText.text = $"Score - {score}";
        timerText.text = $"Time Remaining : {ConvertToTime()}";
        // Possibly remaining customers here (review with otehrs)
    }

    IEnumerator LoseScript(string message, string scence) {
        titleText.text = message;
        yield return new WaitForSeconds(1.5f);
        titleText.text = "";
        // Change scene here. to? Thing?
        SceneManager.LoadScene(scence);
    }

    private void DecoratingUpdate() {
        timeRemaining -= Time.deltaTime;
        UIUpdate();
        DelayedScore();
        LoseUpdate();
        WinUpdate();


        if (currentMask) {
            Mask mask = currentMask.GetComponent<Mask>();
            if (!mask) return;

            mask.DecorateTick(); // Pass in info later
        }

        if (flowerPotsFilled) return;

        OrderData orderData = new OrderData();

        for (int i=0; i<6; i++) {
            GameObject pot = flowerPots[i];

            Flower flower = orderData.flowerArray[i];
            Sprite flowerSprite = flower.GrowingSprites[3];

            for (int j=0; j<Mathf.Clamp(PlayerPrefs.GetInt(flower.Name, 10), 0, 50); j++) {
                float offsetRange = 1f;
                Vector2 randomOffset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-offsetRange, offsetRange) );
                GameObject go = new GameObject();
                go.transform.parent = pot.transform;
                go.transform.position = pot.transform.position;
                go.transform.Translate(randomOffset);
                // Attaching spirte
                go.tag = "MaskScene";
                SpriteRenderer sprRender = go.AddComponent<SpriteRenderer>();
                sprRender.sortingOrder = 2;
                sprRender.sprite = flowerSprite;

                float scale = 0.75f;
                go.transform.localScale = new Vector2(scale, scale);

                go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 2 * Mathf.PI)));
            }

            pot.GetComponent<FlowerPot>().SetAttachedFlower(flower);
        }

        flowerPotsFilled = true;
    }

    private void DelayedScore() {
        int newScore = currentMask.GetComponent<Mask>().GetScore();

        if (newScore > 0) {
            score += (int)(newScore / 10);
            currentMask.GetComponent<Mask>().SetScore(newScore - (int)(newScore / 10));
        }

        PlayerPrefs.SetInt("Money", score);
    }

    private void LoseUpdate() {
        if (!FindFirstObjectByType<Customer>()) return;

        float patience = FindFirstObjectByType<Customer>().patiencePercentage;
        if (patience <= 0) {
            StartCoroutine(LoseScript("You're taking too long!", "Serve"));
        }
    }

    private void WinUpdate() {
        if (currentMask.GetComponent<Mask>().win) {
            StartCoroutine(LoseScript("Good Job!", "Serve"));
        }
    }

    private void Start() {
        SetOrder(FindFirstObjectByType<Customer>().orderOrder);
    }


    private void Update() {
        if (running) DecoratingUpdate();
    }
}
