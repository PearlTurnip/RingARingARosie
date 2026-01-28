using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame() {
		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()) {
			if (!type.IsSubclassOf(typeof(Flower))) {
				continue;
			}
			if (!PlayerPrefs.HasKey(type.Name)) {
				PlayerPrefs.SetInt(type.Name, 10);
			}
		}

			SceneManager.LoadScene("Serve");
	}

	public void QuitGame() {
		Debug.Log("quitting");
		Application.Quit();
	}
}
