using UnityEngine;

public class ItemGlow : MonoBehaviour
{
    public string targetName;
    private SpriteRenderer renderer;
    private Material startingMaterial;
    private bool glowing = false;

    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
        startingMaterial = renderer.material;
    }

    private void Update()
    {
        if (glowing) {
            // Glow here
            renderer.enabled = true;
            renderer.material = Resources.Load<Material>("Materials/PlacementOutline");
        }else {
            renderer.material = startingMaterial;
            renderer.enabled = false;
        }

        glowing = false; // Resetting rather than making updates cuz im lazy
    }

    public void UseGlow() {
        glowing = true;
    }
}
