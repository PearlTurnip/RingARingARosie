using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Collecting UI
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;

    private float timeRemaining = 120f;
    private int score = 0;
    private int remainingCustomers = 3;




    public void Reset() {
        timeRemaining = 120f;

    }

}
