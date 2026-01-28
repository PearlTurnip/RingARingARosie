using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrowingFlower : MonoBehaviour {

	public Flower flower;

	private TextMeshProUGUI flowerText;
	private Image flowerImage;

	public void UpdateText() {
		if (flower == null) {
			flowerText.text = "";
			return;
		}
		else if (flower.DaysGrown >= flower.GrowTime) {
			flowerText.text = "Ready!";
		}
		flowerText.text = $"Water: {flower.WaterGiven / flower.WaterNeeded:P0}";
	}

	public void UpdateSprite() {
		if (flower == null) {
			flowerImage.enabled = false;
			return;
		}

		flowerImage.enabled = true;
		if (flower.DaysGrown == 0) {
			flowerImage.sprite = flower.GrowingSprites[0];
		}
		else if (flower.DaysGrown >= flower.GrowTime) {
			flowerImage.sprite = flower.GrowingSprites[2];
		}
		else {
			flowerImage.sprite = flower.GrowingSprites[1];
		}
	}

	private void OnMouseDrag() {
		if (flower == null) {
			return;
		}
		else if (flower.DaysGrown >= flower.GrowTime) {
			PlayerPrefs.SetInt(flower.Name, PlayerPrefs.GetInt(flower.Name, 0) + 1);
			flower = null;
			UpdateSprite();
			UpdateText();
			return;
		}
		flower.WaterGiven += 0.5f * Time.deltaTime;
		UpdateText();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		flowerText = transform.Find("FlowerText").GetComponent<TextMeshProUGUI>();
		flowerImage = transform.Find("FlowerImage").GetComponent<Image>();
		UpdateText();
		UpdateSprite();
	}

	// Update is called once per frame
	void Update() {

	}
}
