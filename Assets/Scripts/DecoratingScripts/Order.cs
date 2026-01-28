using UnityEngine;
using System;
using System.Collections.Generic;

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

    public int GetAmount() {
        return amount;
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

    public float GetAccuracy(Vector2 position){
        Vector2 closestPoint = validPositions[GetNearestPositionIndex(position)];
        float accuracy = ((closestPoint - position).magnitude / -5f) + 1f;

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
    public Lavender lavender;
    public Rose rose;
    public Sunflower sunflower;
    public Mint mint;
    public Rosemary rosemary;
    public Posies posies;
    public Flower[] flowerArray;


    // Order instances
    public OrderItem lavenderOrder;
    public OrderItem roseNoseOrder;

    public Order test;

    public Dictionary<string, Order> orderMap = new Dictionary<string, Order>();


    public OrderData() {
        lavender = new();
        rose = new();
        sunflower = new();
        mint = new();
        rosemary = new();
        posies = new();
        flowerArray = new Flower[] { lavender, rose, sunflower, mint, rosemary, posies };

        lavenderOrder = new OrderItem(
            lavender,
            2,
            new Vector2[]{
                new Vector2(-1,1), new Vector2(1,-1)
            }
        );

        roseNoseOrder = new OrderItem(
            rose,
            1,
            new Vector2[] {
                new Vector2(0,0)
            }
        );



        test = new Order(
            new OrderItem[] { lavenderOrder, roseNoseOrder }
        );
    }
}