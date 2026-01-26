using UnityEngine;

public class TestShop : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			foreach (GrowingFlower growingFlower in GetComponentsInChildren<GrowingFlower>()) {
				if (growingFlower.flower == null) {
					growingFlower.flower = new Lavendar();
					growingFlower.UpdateText();
					break;
				}
			}
		}
	}
}
