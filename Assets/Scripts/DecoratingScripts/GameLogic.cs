using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct FlowerPotData {
    public string flower;
    public Sprite potTexture;
}

public class GameLogic : MonoBehaviour
{
    // Collecting UI
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject[] flowerPots;

    private bool running = true;

    private float timeRemaining = 120f;
    private int score = 0;

    public FlowerPotData[] potData = new FlowerPotData[6];

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

    private string ConvertToTime() {
        // Doubt we're gonna need anything higher than a minute right?
        int min = (int)(timeRemaining / 60f);
        int sec = (int)(timeRemaining % 60f);

        if (timeRemaining <= 0) return "0:00";
        return $"{min}:{sec}";
    }

    private void UIUpdate() {
        scoreText.text = $"Score - {score}";
        timerText.text = $"Time Remaining : {ConvertToTime()}";
        // Possibly remaining customers here (review with otehrs)
    }

    private void DecoratingUpdate() {
        timeRemaining -= Time.deltaTime;
        UIUpdate();
        
        if (currentMask) {
            Mask mask = currentMask.GetComponent<Mask>();
            if (!mask) return;

            mask.DecorateTick(); // Pass in info later
        }

        for (int i=0; i<6; i++) {
            GameObject pot = flowerPots[i];
            FlowerPotData data = potData[i];
            pot.GetComponent<SpriteRenderer>().sprite = data.potTexture;
            pot.GetComponent<FlowerPot>().SetAttachedFlower(new OrderData().daisy);
        }
    }

    private void Start() {
        currentMask.GetComponent<Mask>().SetCurrentOrder(new OrderData().test);
    }


    private void Update() {
        if (running) DecoratingUpdate();
    }
}
