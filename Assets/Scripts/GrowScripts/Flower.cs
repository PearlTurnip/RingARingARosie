using UnityEngine;

public class Flower
{
	private string name;
	private string description;
	private int price;

	private int growTime;
	private int waterNeeded;
	private float waterGiven = 0;
	private int growStage = 0;

	private Sprite[] sprites;
	private Sprite[] decoratingSprites;


	public string Name { get => name; set => name = value; }
	public string Description { get => description; set => description = value; }
	public int Price { get => price; set => price = value; }
	public int GrowTime { get => growTime; set => growTime = value; }
	public int WaterNeeded { get => waterNeeded; set => waterNeeded = value; }
	public float WaterGiven { get => waterGiven; set => waterGiven = value; }
	public int GrowStage { get => growStage; set => growStage = value; }
    public Sprite[] Sprites { get => sprites; set => sprites = value; }
    public Sprite[] DecoratingSprites { get => decoratingSprites; set => decoratingSprites = value; }
}

public class Lavendar : Flower {
	public Lavendar() {
		Name = "Lavendar";
		Description = "Smells good :D";
		Price = 100;

		GrowTime = 3;
		WaterNeeded = 1;

	}
}