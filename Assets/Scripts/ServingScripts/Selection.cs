using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    [SerializeField]
    Customer customer;

    public void OnMouseDown()
    {
        DontDestroyOnLoad(customer);
        SceneManager.LoadScene("MaskDecoratingSCene");
        customer.transform.position = new Vector2(0, -2);

        //GameLogic game = GameObject.FindGameObjectWithTag("MainGame").GetComponent<GameLogic>();
        //Debug.Log(game);
        //game.SetOrder(customer.orderOrder);

        transform.gameObject.SetActive(false);
    }
}
