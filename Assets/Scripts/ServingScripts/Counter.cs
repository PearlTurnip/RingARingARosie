using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool[] seating;
    public Vector2[] seatPositions;
    [SerializeField]
    CustomerManager customerManager;

    private void Start() {
        seating = new bool[5];
        seatPositions = new Vector2[5]; 
        float w = Camera.main.orthographicSize * 2 * (Screen.width / Screen.height);
        for (int i = 0; i < seating.Length; i++) {
            seatPositions[i] = new Vector2((i + 1) * w / 3f - w, 0f);
        }
    }

    private void Update()
    {
        bool flag = false;
        for (int i = 0; i < seating.Length; i++){
            if (seating[i])
            {
                flag = true;
            }
        }
        if (!flag && customerManager.waitingForDayToEnd) //all seats are empty
        {
            customerManager.day++;
            PlayerPrefs.SetInt("Day", customerManager.day);
        }
    }
}
