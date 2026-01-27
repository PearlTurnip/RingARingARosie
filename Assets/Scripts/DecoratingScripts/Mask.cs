using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    private Order currentOrder;
    

    private int selectedOrderItemIndex;

    private List<ItemGlow> highlightObjects = new List<ItemGlow>();


    public void SetCurrentOrder(Order newOrder) {
        currentOrder = newOrder;
    }


    public void PlaceHighlights() {
        foreach (OrderItem item in currentOrder.GetItems() ){
            for (int i=0; i<item.GetValidPositions().Length; i++) {
                Vector2 highlightPos = item.GetValidPositions()[i];

                // Place highlighted sprite here
                GameObject itemObject = new GameObject();

                // Creating the glow for the items
                SpriteRenderer renderer = itemObject.AddComponent<SpriteRenderer>();
                renderer.sprite = item.GetSprite();

                // Creating highlight glow
                ItemGlow glow = itemObject.AddComponent<ItemGlow>();
                glow.targetName = item.GetName();
                highlightObjects.Add(glow);
            }
        }
    }

    private void GlowUpdate() {
        foreach (ItemGlow glow in highlightObjects) {
            if (glow.targetName == currentOrder.GetItems()[selectedOrderItemIndex].GetName()) {
                glow.UseGlow();
            }
        }
    }

    private void FlowerPotUpdate() {
        if (Input.GetMouseButton(0)) {
            Vector2 mouseSize = new(10f, 10f); // Used a size of 10 for some leniency. Make it feel nicer
            RaycastHit2D[] hits = Physics2D.BoxCastAll(Input.mousePosition, mouseSize, 0f, Vector2.zero, 1f, 0);
            foreach (RaycastHit2D hit in hits) {
                if (!hit.collider.CompareTag("FlowerBox")) continue;
                if (!hit.collider.TryGetComponent<FlowerPot>(out FlowerPot potScript)) continue;

                potScript.GetAttachedFlower();
                
            }            
        }
    }


    public void DecorateTick() {
        GlowUpdate();
        FlowerPotUpdate();
    }
}
