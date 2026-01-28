using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    [SerializeField]
    Customer customer;

    public void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(customer.orderOrder);
            SceneManager.LoadScene("MaskDecoratingSCene");
        }
    }
}
