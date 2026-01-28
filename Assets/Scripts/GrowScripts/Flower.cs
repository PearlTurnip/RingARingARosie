using UnityEngine;

public class Flower
{
	private string name;
	private string description;
	private int price;

	private int growTime;
	private int daysGrown = 0;

	private float waterNeeded;
	private float waterGiven = 0;

	private Sprite[] growingSprites;


	public string Name { get => name; set => name = value; }
	public string Description { get => description; set => description = value; }
	public int Price { get => price; set => price = value; }
	public int GrowTime { get => growTime; set => growTime = value; }
	public int DaysGrown { get => daysGrown; set => daysGrown = value; }
	public float WaterNeeded { get => waterNeeded; set => waterNeeded = value; }
	public float WaterGiven { get => waterGiven; set => waterGiven = value; }
    public Sprite[] GrowingSprites { get => growingSprites; set => growingSprites = value; }
}

public class Lavendar : Flower {
	public Lavendar() {
		Name = "Lavendar";
		Description = "Smells good :D";
		Price = 100;

		GrowTime = 3;
		WaterNeeded = 1;

		GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant4");
	}
}

public class Rose : Flower {
    public Rose() {
        Name = "Rose";
        Description = "Rose? I hardly know her!";
        Price = 100;

        GrowTime = 3;
        WaterNeeded = 1;

        GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant1");
    }
}

public class Sunflower : Flower {
    public Sunflower() {
        Name = "Sunflower";
        Description = "Always on the sunny side of life"; // Or (why is there not a moon flower)
        Price = 100;

        GrowTime = 3;
        WaterNeeded = 1;

        GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant2");
    }
}

public class Mint : Flower {
    public Mint() {
        Name = "Mint";
        Description = "Refreshing as always";
        Price = 100;

        GrowTime = 3;
        WaterNeeded = 1;

        GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant5");
    }
}

public class Rosemary : Flower {
    public Rosemary() {
        Name = "Rosemary";
        Description = "The grandmother of the plant world";
        Price = 100;

        GrowTime = 3;
        WaterNeeded = 1;

        GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant6");
    }
}

public class Posies : Flower {
    public Posies() {
        Name = "Rosemary";
        Description = "A-tishoo! A-tishoo!\nWe all fall down!";
        Price = 100;

        GrowTime = 3;
        WaterNeeded = 1;

        GrowingSprites = Resources.LoadAll<Sprite>("Sprites/Plants/plant3");
    }
}