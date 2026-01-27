using System.Collections;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private float patiencePercentage = 100f;
    [SerializeField]
    private ProgressBar patience;
    public TextMeshProUGUI moneyUI;
    public bool seated = false;
    public int seat = -1;
    public bool hasBeenServed = false;
    public Counter counter;

    private void Start() {
        transform.position = new Vector2(-20, 0);
    }

    private void GenerateOrder() {
        //generate the order that the customer wants and add it to the orders list
    }

    private void EnterScene() {
        //move the customer to an empty position in the scene
        for (int i = 0; i < counter.seating.Length; i++) {
            if (counter.seating[i] == false) {
                counter.seating[i] = true;
                seat = i;
                StartCoroutine(Move(-20f, counter.seatPositions[i].x));
                seated = true;
                GenerateOrder();
                break;
            }
        }
    }

    private void ExitScene(bool goodMood) {
        //move the customer off the scene
        float startPos = transform.position.x;
        StartCoroutine(Move(startPos, -20f));
        counter.seating[seat] = false;
        Destroy(gameObject, 3);
    }

    IEnumerator Move(float start, float end) {
        float t = 0;
        while (t < 3f) {
            transform.position = new Vector2(Mathf.Lerp(start, end, t), 0f);
            t += Time.deltaTime; 
            yield return null;
        }
        
    }

    private void Update() {
        if (seated) {
            if (hasBeenServed) {
                //Pay the Player
                string currentMoney = moneyUI.text;
                currentMoney = currentMoney.Remove(0, 5);
                int money = int.Parse(currentMoney); 
                money += Mathf.FloorToInt(patiencePercentage);
                moneyUI.text = "$$ - " + money.ToString();

                ExitScene(true);
                seated = false;
            }

            patiencePercentage -= Time.deltaTime;
            patience.value = patiencePercentage;
            if (patiencePercentage < 0) {
                ExitScene(false);
                seated = false;
            }
        }
        else if (seat == -1)
        {
            EnterScene();
        }
    }
}
