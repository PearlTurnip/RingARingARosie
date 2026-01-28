using UnityEngine;

public class ItemGlow : MonoBehaviour
{
    public string targetName;
    private Renderer renderer;
    private Material startingMaterial;
    private bool glowing = false;

    private void Start() {
        renderer = GetComponent<Renderer>();
        startingMaterial = renderer.material;
    }

    private void Update()
    {
        if (glowing) {
            // Glow here
            renderer.material = Resources.Load<Material>("Materials/PlacementOutline.mat");
        }else {
            renderer.material = startingMaterial;
        }

        glowing = false; // Resetting rather than making updates cuz im lazy
    }

    public void UseGlow() {
        glowing = true;
    }
}
