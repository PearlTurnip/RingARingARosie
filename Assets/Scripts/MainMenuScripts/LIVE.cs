using UnityEngine;

public class LIVE : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
