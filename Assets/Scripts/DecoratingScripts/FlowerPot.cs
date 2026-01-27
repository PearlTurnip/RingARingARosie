using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    private Flower attachedFlower;

    public Flower GetAttachedFlower() {
        return attachedFlower;
    }

    public void SetAttachedFlower(Flower newFlower) {
        attachedFlower = newFlower;
    }
}
