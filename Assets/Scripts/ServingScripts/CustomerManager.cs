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
    private int runningDay = -1;
    public bool waitingForDayToEnd = false;

    private void Start()
    {
        runningDay = day - 1;
    }

    public void spawnCustomer()
    {
        Customer newCustomer = Instantiate(customerPrefab);
        newCustomer.counter = counter;
        newCustomer.moneyUI = moneyUI;
    }

    IEnumerator StartDay(int customers)
    {
        waitingForDayToEnd = false;
        int customersSent = 0;
        while (customersSent < customers)
        {
            spawnCustomer();
            customersSent++;
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
        waitingForDayToEnd = true;
    }

    private void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        spawnCustomer();
    //    }

        if (day != runningDay)
        {
            dayUI.text = "Day - " + day.ToString();
            //Debug.Log(runningDay);
            if (day < 10)
            {
                StartCoroutine(StartDay(day + 2));
            }
            else
            {
                StartCoroutine(StartDay(2* day));
            }

            runningDay++;
        }
    }
}
