using UnityEngine;

public class Flower
{
    public string name;
    public string description;
    public int price;

    public int growTime;
    public int waterNeeded;
    public float waterGiven = 0;
    public int growStage = 0;

    public Sprite[] images;
}

public class Lavendar : Flower {
    public Lavendar() {
        name = "Lavendar";
        description = "Smells good :D";
        price = 100;

        growTime = 3;
        waterNeeded = 1;

    }
}