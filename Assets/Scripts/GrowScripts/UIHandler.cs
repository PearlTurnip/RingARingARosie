using System;
using TMPro;
using System.Reflection;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	private GameObject store;
	private bool storeOpen = false;

	private IEnumerator LerpStorePanel() {
		float t = 0;
		float startingPosition = store.transform.position.x;
		float goalPosition = storeOpen ? startingPosition - 230 : startingPosition + 230;
		while (t < 1) {
			store.transform.position = new Vector3(Mathf.Lerp(startingPosition, goalPosition, t), store.transform.position.y, store.transform.position.z);
			t += Time.deltaTime * 5;
			yield return null;
		}
	}

	public void ToggleStorePanel() {
		storeOpen = !storeOpen;
		StartCoroutine(LerpStorePanel());
	}

	public void PurchaseFlower(Type flower) {
		foreach (GrowingFlower growingFlower in FindObjectsByType<GrowingFlower>(FindObjectsSortMode.None)) {
			if (growingFlower.flower == null) {
				growingFlower.flower = (Flower)Activator.CreateInstance(flower);
				growingFlower.UpdateText();
				break;
			}
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		store = transform.Find("StoreTab").gameObject;

		GameObject purchaseableFlower = Resources.Load<GameObject>("Prefabs/PurchaseableFlower");

		int flowerCount = 0;
		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()) {
			if (!type.IsSubclassOf(typeof(Flower))) {
				continue;
			}

			Flower flower = (Flower)Activator.CreateInstance(type);

			Transform newPurchaseableFlower = Instantiate(purchaseableFlower, store.transform).transform;

			newPurchaseableFlower.localPosition = new Vector3(0, 102 - (120 * flowerCount), 0);
			newPurchaseableFlower.Find("FlowerSprite").GetComponent<Image>().sprite = flower.GrowingSprites[0];
			newPurchaseableFlower.Find("FlowerNameText").GetComponent<TextMeshProUGUI>().text = flower.Name;
			newPurchaseableFlower.Find("FlowerCostText").GetComponent<TextMeshProUGUI>().text = $"{flower.Price}";
			newPurchaseableFlower.Find("PurchaseFlowerButton").GetComponent<Button>().onClick.AddListener(() => PurchaseFlower(type));
			flowerCount++;
		}

	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
