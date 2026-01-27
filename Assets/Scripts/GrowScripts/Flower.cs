using UnityEngine;

public class Flower
{
	private string name;
	private string description;
	private int price;

	private int growTime;
	private float waterNeeded;
	private float waterGiven = 0;
	private int growStage = 0;

	private Sprite[] growingSprites;
	private Sprite[] decoratingSprites;


	public string Name { get => name; set => name = value; }
	public string Description { get => description; set => description = value; }
	public int Price { get => price; set => price = value; }
	public int GrowTime { get => growTime; set => growTime = value; }
	public float WaterNeeded { get => waterNeeded; set => waterNeeded = value; }
	public float WaterGiven { get => waterGiven; set => waterGiven = value; }
	public int GrowStage { get => growStage; set => growStage = value; }
    public Sprite[] GrowingSprites { get => growingSprites; set => growingSprites = value; }
    public Sprite[] DecoratingSprites { get => decoratingSprites; set => decoratingSprites = value; }
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

public class Daisy : Flower {
	public Daisy() {
		Name = "Daisy";
		Description = "Daisy? I hardly know her";
		Price = 20;

		GrowTime = 2;
		WaterNeeded = 0.5f;

		GrowingSprites = new Sprite[] { UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Plants/plant4_0.png"), UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Plants/plant4_1.png"), UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Plants/plant4_2.png") };
	}
}