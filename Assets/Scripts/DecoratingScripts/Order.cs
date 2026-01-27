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
        return flower.DecoratingSprites[0];
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

    public Order(Sprite maskSprite, OrderItem[] _items) {
        mask = maskSprite;
        items = _items;
    }

    public OrderItem[] GetItems(){
        return items;
    }
}

public class Daisy : Flower {
    public Daisy() {
        Name = "Daisy";
        Description = "Daisy? I hardly know her";
        Price = 20;

        GrowTime = 2;
        WaterNeeded = 0;

        DecoratingSprites = new Sprite[] {
            Resources.Load<Sprite>("Sprites/FlowerPots/daisy_pot"),
            Resources.Load<Sprite>("Sprites/FlowerPots/daisy_pot")
        };
    }
}

public class OrderData {
    // Creating flower instances
    public Daisy daisy;
    public OrderItem daisyOrder;

    public Order test;


    public OrderData() {
        daisy = new();
        daisyOrder = new OrderItem(
            daisy,
            2,
            new Vector2[]{
                new Vector2(-1,1), new Vector2(1,-1)
            }
        );

        test = new Order(
            Resources.Load<Sprite>("Sprites/Picture1"),
            new OrderItem[] { daisyOrder }
        );

    }
}