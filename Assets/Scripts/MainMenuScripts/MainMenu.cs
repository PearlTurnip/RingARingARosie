using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame() {
		SceneManager.LoadScene("Serve");
	}

	public void QuitGame() {
		Debug.Log("quitting");
		Application.Quit();
	}
}
