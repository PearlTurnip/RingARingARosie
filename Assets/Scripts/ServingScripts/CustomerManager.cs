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
            Debug.Log(runningDay);
            switch (day)
            {
                case 0:
                    StartCoroutine(StartDay(5));
                    break;
                case 1:
                    StartCoroutine(StartDay(6));
                    break;
                case 2:
                    StartCoroutine(StartDay(8));
                    break;
                case 3:
                    StartCoroutine(StartDay(10));
                    break;
                case 4:
                    StartCoroutine(StartDay(12));
                    break;
                case 5:
                    StartCoroutine(StartDay(7));
                    break;
                case 6:
                    StartCoroutine(StartDay(15));
                    break;
                case 7:
                    StartCoroutine(StartDay(20));
                    break;

                default:
                    StartCoroutine(StartDay(25));
                    break;
            }

            runningDay++;
        }
    }
}
