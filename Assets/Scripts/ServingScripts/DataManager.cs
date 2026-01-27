using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public int money;
    public int day;

    [SerializeField]
    private CustomerManager customerManager;
    [SerializeField]
    private TextMeshProUGUI moneyUI;

    void Start()
    {
        //universal variables
        money = PlayerPrefs.GetInt("Money");
        day = PlayerPrefs.GetInt("Day");

        //send data to appropriate places
        moneyUI.text = "$$ - " + money.ToString();
        customerManager.day = day;
    }
}
