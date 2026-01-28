using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField]
    public float patiencePercentage = 100f;
    [SerializeField]
    private ProgressBar patience;
    [SerializeField]
    private SpriteRenderer bar;
    [SerializeField]
    private Sprite[] characters;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer textBubble;
    [SerializeField]
    private TextMeshPro orderText;

    public Order orderOrder;
    public string[] flowers;

    public TextMeshProUGUI moneyUI;
    public bool seated = false;
    public int seat = -1;
    public bool hasBeenServed = false;
    public Counter counter;

    private void Start() {
        transform.position = new Vector2(-20f, -2f);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = characters[Mathf.FloorToInt(Random.Range(0, characters.Length))];
        orderText = gameObject.GetComponentInChildren<TextMeshPro>();
        textBubble.enabled = false;
        orderText.enabled = false;
        patience.GetComponentInParent<SpriteRenderer>().enabled = false;
        bar.enabled = false;
    }
    private void GenerateOrder(float difficulty) //difficulty between 0-1
        { 
        //generate the order that the customer wants and add it to the orders list
        int howManyTypesOfFlowerAreThere = flowers.Length;
        textBubble.enabled = true;
        orderText.enabled = true;
        patience.GetComponentInParent<SpriteRenderer>().enabled = true;
        bar.enabled = true;
        orderText.text = "A mask with:\n";

        List<Vector2> positions = new List<Vector2>();

        int rigged = Mathf.FloorToInt(Random.Range(-0.5f + difficulty, howManyTypesOfFlowerAreThere - 1));
        Dictionary<int, string> order = new Dictionary<int, string>();
        for (int i = 0; i < howManyTypesOfFlowerAreThere; i++)
        {
            float randV = Random.Range(0f, 1f);
             if ((0.5f <= randV && randV < 0.8f) || rigged == i)
            {
                order.Add(i, "nose");
                positions.Add(new Vector2(0, -3));
            }
            else if (randV < 0.6f)
            {
                order.Add(i, "none");
                positions.Add(new Vector2(-20, -20));
            }
            else if (randV < 0.7f)
            {
                order.Add(i, "left cheek");
                positions.Add(new Vector2(1, -3));
            }
            else if (randV < 0.8f)
            {
                order.Add(i, "right cheek");
                positions.Add(new Vector2(-1, -3));
            }
            else if (randV < 0.9f)
            {
                order.Add(i, "left eye");
                positions.Add(new Vector2(1, -2));
            }
            else
            {
                order.Add(i, "right eye");
                positions.Add(new Vector2(-1, -2));
            }
        }

        List<OrderItem> orderItems = new List<OrderItem>();
        Vector2[] pos;
        for (int i = 0; i < howManyTypesOfFlowerAreThere; i++)
        {
            if (order[i] != "none")
            {
                orderText.text += flowers[i] + " " + order[i] + "\n";

                pos = new Vector2[1];
                pos[0] = positions[i];
                switch (flowers[i])
                {
                    case "Rose":
                        Rose rose = new Rose();
                        orderItems.Add(new OrderItem(rose, 1, pos));
                        break;
                    case "Sunflower":
                        Sunflower sunflower = new Sunflower();
                        orderItems.Add(new OrderItem(sunflower, 1, pos));
                        break;
                    case "Mint":
                        Mint mint = new Mint();
                        orderItems.Add(new OrderItem(mint, 1, pos));
                        break;
                    case "Lavender":
                        Lavender lavender = new Lavender();
                        orderItems.Add(new OrderItem(lavender, 1, pos));
                        break;
                    case "Rosemary":
                        Rosemary rosemary = new Rosemary();
                        orderItems.Add(new OrderItem(rosemary, 1, pos));
                        break;
                    case "Posie":
                        Posies posie = new Posies();
                        orderItems.Add(new OrderItem(posie, 1, pos));
                        break;
                }
            }
        }
        orderOrder = new Order(orderItems.ToArray());
    }

    private void RemoveOrder(){
        textBubble.enabled = false;
        orderText.enabled = false;
        patience.GetComponentInParent<SpriteRenderer>().enabled = false;
        bar.enabled = false;
    }

    private void EnterScene() {
        //move the customer to an empty position in the scene
        for (int i = 0; i < counter.seating.Length; i++) {
            if (counter.seating[i] == false) {
                counter.seating[i] = true;
                seat = i;
                StartCoroutine(Move(-20f, counter.seatPositions[i].x, true));
                seated = true;
                break;
            }
        }
    }

    private void ExitScene(bool goodMood) {
        //move the customer off the scene
        float startPos = transform.position.x;
        StartCoroutine(Move(startPos, -20f, false));
        counter.seating[seat] = false;

        int temp = PlayerPrefs.GetInt("CustomersServedToday");
        PlayerPrefs.SetInt("CustomersServedToday", temp++);
        PlayerPrefs.Save();

        Destroy(gameObject, 1);
    }

    IEnumerator Move(float start, float end, bool entering) {
        if (!entering)
        {
            RemoveOrder();
        }

        float t = 0;
        while (t < 1f) {
            transform.position = new Vector2(Mathf.Lerp(start, end, t), -2f);
            t += Time.deltaTime; 
            yield return null;
        }

        if (entering)
        {
            GenerateOrder(0);
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

                PlayerPrefs.SetInt("Money", money);
                PlayerPrefs.Save();

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
