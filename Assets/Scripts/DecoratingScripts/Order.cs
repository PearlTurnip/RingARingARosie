using UnityEngine;
using UnityEngine.InputSystem;

public class OrderItem {
    private Flower flower;
    private int amount;
    private Vector2[] validPositions;

    public OrderItem(Flower _flower, int amt, Vector2[] positions) {
        flower = _flower;
        amount = amt;
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
        return flower.GrowingSprites[3];
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
    OrderItem[] items;

    public Order(OrderItem[] _items) {
        items = _items;
    }

    public OrderItem[] GetItems(){
        return items;
    }
}


public class OrderData {
    // Creating flower instances
    public Lavendar lavendar;
    public Rose rose;
    public Sunflower sunflower;
    public Mint mint;
    public Rosemary rosemary;
    public Posies posies;
    public Flower[] flowerArray;


    // Order instances
    public OrderItem lavendarOrder;

    public Order test;


    public OrderData() {
        lavendar = new();
        rose = new();
        sunflower = new();
        mint = new();
        rosemary = new();
        posies = new();
        flowerArray = new Flower[] { lavendar, rose, sunflower, mint, rosemary, posies };

        lavendarOrder = new OrderItem(
            lavendar,
            2,
            new Vector2[]{
                new Vector2(-1,1), new Vector2(1,-1)
            }
        );

        test = new Order(
            new OrderItem[] { lavendarOrder }
        );

    }
}