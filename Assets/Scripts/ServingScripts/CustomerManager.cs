using System.Collections;
using UnityEngine;
using TMPro;
public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer customerPrefab;
    [SerializeField]
    private Counter counter;
    [SerializeField]
    private TextMeshProUGUI moneyUI;
    [SerializeField]
    private TextMeshProUGUI dayUI;

    public int day = 0;
    public int customersSentToday = 0;
    public int customersServedToday = 0;
    public bool waitingForDayToEnd = false;

    private void Start()
    {
        customersSentToday = PlayerPrefs.GetInt("CustomersSentToday");
        customersServedToday = PlayerPrefs.GetInt("CustomersServedToday");

        if (customersSentToday == 0)
        {
            dayUI.text = "Day - " + day.ToString();
            //Debug.Log(runningDay);
            StartCoroutine(StartDay(day + 2));

        }
        else if (customersServedToday < day + 2)
        {
            int temp = customersSentToday - customersServedToday;
            for (int i = 0; i < temp; i++)
            {
                customersSentToday--;
                spawnCustomer();
            }
        }
    }

    public void spawnCustomer()
    {
        customersSentToday++;
        PlayerPrefs.SetInt("CustomersSentToday", customersSentToday);
        PlayerPrefs.Save();
        Customer newCustomer = Instantiate(customerPrefab);
        newCustomer.counter = counter;
        newCustomer.moneyUI = moneyUI;
    }

    IEnumerator StartDay(int customers)
    {
        waitingForDayToEnd = false;
        while (customersSentToday < customers)
        {
            spawnCustomer();
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
        waitingForDayToEnd = true;
    }
}
