using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    private Order currentOrder;
    

    private int selectedOrderItemIndex;

    private bool debounce = false;

    private GameObject heldFlower;

    private List<ItemGlow> highlightObjects = new List<ItemGlow>();

    private List<GameObject> placedFlowers = new List<GameObject>();


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
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseSize = new(0.5f, 0.5f); // Used a size of 10 for some leniency. Make it feel nicer
        if (Input.GetMouseButton(0)) {
            if (heldFlower == null) {

                Collider2D[] hits = Physics2D.OverlapBoxAll(mouseWorld, mouseSize, 0f);
                foreach (Collider2D hit in hits) {
                    if (!hit.CompareTag("FlowerPot")) continue;
                    if (!hit.TryGetComponent<FlowerPot>(out FlowerPot potScript)) continue;

                    Flower attFlower = potScript.GetAttachedFlower();

                    heldFlower = new GameObject();
                    SpriteRenderer spriteRender = heldFlower.AddComponent<SpriteRenderer>();
                    spriteRender.sprite = attFlower.DecoratingSprites[0];
                    spriteRender.sortingOrder = 5;
                    heldFlower.transform.position = mouseWorld;

                }
            }else {
                heldFlower.transform.position = mouseWorld;
            }

            debounce = true;
        }else {
            // Placement logic
            if (debounce) {
                // Only worry about placement here
                Collider2D[] hits = Physics2D.OverlapBoxAll(mouseWorld, mouseSize, 0f);
                bool didCollide = false;
                foreach (Collider2D hit in hits) {
                    if (!hit.CompareTag("Mask")) continue;

                    didCollide = true;
                    placedFlowers.Add(heldFlower);
                }

                if (!didCollide) Destroy(heldFlower);
                heldFlower = null; // Using for resetting + safety

                debounce = false;
            }
        }
    }


    public void DecorateTick() {
        GlowUpdate();
        FlowerPotUpdate();
    }
}
