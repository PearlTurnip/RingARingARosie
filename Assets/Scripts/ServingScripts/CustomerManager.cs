using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer customerPrefab;
    [SerializeField]
    private Counter counter;
    [SerializeField]
    private TextMeshProUGUI moneyUI;

    public void spawnCustomer()
    {
        Customer newCustomer = Instantiate(customerPrefab);
        newCustomer.counter = counter;
        newCustomer.moneyUI = moneyUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnCustomer();
        }
    }
}
