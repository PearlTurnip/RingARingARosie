using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool[] seating;
    public Vector2[] seatPositions;

    private void Start() {
        seating = new bool[6];
        seatPositions = new Vector2[6]; 
        float w = Camera.main.orthographicSize * 2 * (Screen.width / Screen.height);
        for (int i = 0; i < seating.Length; i++) {
            seatPositions[i] = new Vector2(i * w / 6f - w / 2f, 0f);
        }
    }
}
