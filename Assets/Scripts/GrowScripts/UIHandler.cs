using System;
using TMPro;
using System.Reflection;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {
	[SerializeField]
	private GameObject store;
	[SerializeField]
	private Transform storeOpenPosition;
	[SerializeField]
	private Transform storeClosedPosition;
	private bool storeOpen = false;
	private bool storeMoving;

	private int money;
	private int day;
	[SerializeField]
	private TextMeshProUGUI dayAndMoneyText;

	[SerializeField]
	private GameObject planter;

	private IEnumerator LerpStorePanel() {
		float t = 0;
		float startingPosition = store.transform.position.x;
		float goalPosition = storeOpen ? storeOpenPosition.position.x : storeClosedPosition.position.x;
		storeMoving = true;
		while (t < 1) {
			store.transform.position = new Vector3(Mathf.Lerp(startingPosition, goalPosition, t), store.transform.position.y, store.transform.position.z);
			t += Time.deltaTime * 5;
			yield return null;
		}
		storeMoving = false;
	}

	public void ToggleStorePanel() {
		if (storeMoving) {
			return;
		}
		storeOpen = !storeOpen;
		StartCoroutine(LerpStorePanel());
	}

	public void PurchaseFlower(Type flower) {
		//Basically just doing "new Flower()" but in a more annoying way
		Flower flowerInstance = (Flower)Activator.CreateInstance(flower);

		//If you can't afford the seed then cancel the purchase
		if (money < flowerInstance.Price) {
			return;
		}

		GrowingFlower[] growingFlowers = planter.transform.GetComponentsInChildren<GrowingFlower>().OrderBy(o => transform.name).ToArray();

		foreach (GrowingFlower growingFlower in growingFlowers) {
			if (growingFlower.flower == null) {
				growingFlower.flower = flowerInstance;
				growingFlower.UpdateText();
				growingFlower.UpdateSprite();
				break;
			}
		}
		money -= flowerInstance.Price;
		dayAndMoneyText.text = $"Day {day}\n${money}";
	}

	public void EndDay() {
		day++;

		GrowingFlower[] growingFlowers = planter.transform.GetComponentsInChildren<GrowingFlower>().OrderBy(o => transform.name).ToArray();

		foreach (GrowingFlower growingFlower in growingFlowers) {
			if (growingFlower.flower != null) {
				if (growingFlower.flower.WaterGiven / growingFlower.flower.WaterNeeded > 0.90 && growingFlower.flower.WaterGiven / growingFlower.flower.WaterNeeded < 1.1) {
					growingFlower.flower.DaysGrown++;
				}
				PlayerPrefs.SetString(growingFlower.name, JsonUtility.ToJson(growingFlower.flower));
			}
		}

		PlayerPrefs.SetInt("Day", day);
		PlayerPrefs.SetInt("Money", money);
		PlayerPrefs.Save();
		SceneManager.LoadScene("Serve");
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		store.transform.position = storeClosedPosition.transform.position;
		Transform storeContent = store.transform.Find("Viewport").Find("Content");

		money = PlayerPrefs.GetInt("Money");
		day = PlayerPrefs.GetInt("Day");

		dayAndMoneyText.text = $"Day {day}\n${money}";

		GameObject purchaseableFlower = Resources.Load<GameObject>("Prefabs/PurchaseableFlower");

		//Get all types that are children of the flower type, then create an object for each of them in the shop
		int flowerCount = 0;
		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()) {
			if (!type.IsSubclassOf(typeof(Flower))) {
				continue;
			}

			//Basically just doing "new Flower()" but in a more annoying way
			Flower flower = (Flower)Activator.CreateInstance(type);

			Transform newPurchaseableFlower = Instantiate(purchaseableFlower, storeContent).transform;

			newPurchaseableFlower.Find("FlowerSprite").GetComponent<Image>().sprite = flower.GrowingSprites[0];
			newPurchaseableFlower.Find("FlowerNameText").GetComponent<TextMeshProUGUI>().text = flower.Name;
			newPurchaseableFlower.Find("FlowerCostText").GetComponent<TextMeshProUGUI>().text = $"${flower.Price}";
			newPurchaseableFlower.Find("PurchaseFlowerButton").GetComponent<Button>().onClick.AddListener(() => PurchaseFlower(type));
			flowerCount++;
		}

		GrowingFlower[] growingFlowers = planter.transform.GetComponentsInChildren<GrowingFlower>().OrderBy(o => transform.name).ToArray();

		foreach (GrowingFlower growingFlower in growingFlowers) {
			if (!PlayerPrefs.HasKey(growingFlower.name)) {
				continue;
			}
			Flower savedFlower = JsonUtility.FromJson<Flower>(PlayerPrefs.GetString(growingFlower.name));
			growingFlower.flower = (Flower)Activator.CreateInstance(Type.GetType(savedFlower.Name));
			growingFlower.flower.DaysGrown = savedFlower.DaysGrown;
			growingFlower.UpdateSprite();
			growingFlower.UpdateText();
		}

	}

	// Update is called once per frame
	void Update() {

	}
}
