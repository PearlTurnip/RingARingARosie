using System;
using TMPro;
using UnityEngine;

public class GrowingFlower : MonoBehaviour
{

	public Flower flower;

	[SerializeField]
	private TextMeshProUGUI flowerText;

	public void UpdateText() {
		if (flower == null) {
			flowerText.text = "Empty";
			return;
		}
		flowerText.text = $"{flower.Name}\nWater: {flower.WaterGiven / flower.WaterNeeded:N0}%";
	}

	private void OnMouseDrag() {
		flower.WaterGiven += 50 * Time.deltaTime;
		UpdateText();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		UpdateText();

		//flower = new Lavendar();
		//flowerText.text = $"{flower.name}\nWater: {flower.waterGiven / flower.waterNeeded:N0}%";
	}

	// Update is called once per frame
	void Update()
	{

	}
}
