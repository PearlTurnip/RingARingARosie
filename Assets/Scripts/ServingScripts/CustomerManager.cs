using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Customer customerPrefab;
    [SerializeField]
    private Counter counter;

    public void spawnCustomer()
    {
        Customer newCustomer = Instantiate(customerPrefab);
        newCustomer.counter = counter;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnCustomer();
        }
    }
}
