using Unity.VisualScripting;
using UnityEngine;

public class OrderItem {
    private Flower flower;
    private Vector2[] validPositions;

    OrderItem(Flower _flower, Vector2[] positions) {
        flower = _flower;
        validPositions = positions;
    }

    public string GetName()
    {
        return flower.Name;
    }

    public Vector2[] GetValidPositions(){
        return validPositions;
    }

    public Sprite GetSprite(){
        return flower.GrowingSprites[0];
    }

    int GetNearestPositionIndex(Vector2 position){
        int closest = 0;
        float closestDistance = 1000f;
        for (int i = 0; i < validPositions.Length; i++) {
            float distance = (validPositions[i] - position).magnitude;
            if (distance < closest) {
                closestDistance = distance;
                closest = i;
            }
        }

        return closest;
    }

    float GetAccuracy(Vector2 position){
        Vector2 closestPoint = validPositions[GetNearestPositionIndex(position)];
        float accuracy = ((closestPoint - position).magnitude / -2000f) + 1f;

        return accuracy;
    }
}

public class Order {
    Sprite mask;
    OrderItem[] items;

    OrderItem selectedItem;

    public OrderItem[] GetItems(){
        return items;
    }
}

public class OrderData {
    // Creating flower instances
    public Lavendar lavendar = new();
    //public 
}