using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private float patiencePercentage = 100f;
    public bool seated = false;
    public int seat = -1;
    public bool hasBeenServed = false;
    [SerializeField]
    Counter counter;

    private void Start() {
        transform.position = new Vector2(-20, 0);
        EnterScene();
        GenerateOrder();
    }

    private void GenerateOrder() {
        //generate the order that the customer wants and add it to the orders list
    }

    private void EnterScene() {
        //move the customer to an empty position in the scene
        for (int i = 0; i < counter.seating.Length; i++) {
            if (counter.seating[i] == false) {
                counter.seating[i] = true;
                seat = i;
                StartCoroutine(Move(-20f, counter.seatPositions[i].x));
                seated = true;
                break;
            }
        }
    }

    private void ExitScene(bool goodMood) {
        //move the customer off the scene
        float startPos = transform.position.x;
        counter.seating[seat] = false;
        StartCoroutine(Move(startPos, -20f));
    }

    IEnumerator Move(float start, float end) {
        float t = 0;
        while (t < 3f) {
            Debug.Log(t);
            transform.position = new Vector2(Mathf.Lerp(start, end, t), 0f);
            t += Time.deltaTime; 
            yield return null;
        }
        
    }

    private void Update() {
        if (seated) {
            if (hasBeenServed) {
                //Pay the Player <<<<<
                ExitScene(true);
            }

            patiencePercentage -= Time.deltaTime;
            if (patiencePercentage < 0) {
                ExitScene(false);
            }
        }
    }
}
