using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float value = 100f;

    private void Update()
    {
        if (value > 0f)
        {
            transform.localScale = new Vector3(value / 100f, 1f, 1f);
        }
    }
}
