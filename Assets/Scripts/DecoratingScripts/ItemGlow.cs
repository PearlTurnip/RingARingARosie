using UnityEngine;

public class ItemGlow : MonoBehaviour
{
    public string targetName;

    private bool glowing = false;


    void Update()
    {
        if (glowing) {
            // Glow here
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (Mathf.Cos(Time.time) + 1f) / 2f);
        }

        glowing = false; // Resetting rather than making updates cuz im lazy
    }

    public void UseGlow() {
        glowing = true;
    }
}
