using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool[] seating;
    public Vector2[] seatPositions;

    private void Start() {
        seating = new bool[6];
        seatPositions = new Vector2[6];
        for (int i = 0; i < seating.Length; i++) {
            seatPositions[i] = new Vector2(i*2f - 8f, 0f);
        }
    }
}
