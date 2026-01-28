using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    private Order currentOrder;
    private int selectedOrderItemIndex;

    private bool debounce = false;

    private int placedFlowers = 0;
    private int score = 0;

    private GameObject heldFlower;

    private List<ItemGlow> highlightObjects = new List<ItemGlow>();


    public void SetScore(int amt) {
        score = amt;
    }

    public int GetScore() {
        return score;
    }


    public void SetCurrentOrder(Order newOrder) {
        currentOrder = newOrder;
        selectedOrderItemIndex = 0;
        PlaceHighlights();
        placedFlowers = 0;
    }


    public void PlaceHighlights() {
        highlightObjects.Clear();
        foreach (OrderItem item in currentOrder.GetItems() ){
            for (int i=0; i<item.GetValidPositions().Length; i++) {
                Vector2 highlightPos = item.GetValidPositions()[i];

                // Place highlighted sprite here
                GameObject itemObject = new GameObject();
                itemObject.tag = "MaskScene";

                // Creating the glow for the items
                SpriteRenderer renderer = itemObject.AddComponent<SpriteRenderer>();
                renderer.sortingOrder = 4;
                renderer.sprite = item.GetSprite();

                itemObject.transform.localScale = new Vector2(4, 4);
                itemObject.transform.position = highlightPos;

                // Creating highlight glow
                ItemGlow glow = itemObject.AddComponent<ItemGlow>();
                glow.targetName = item.GetName();
                highlightObjects.Add(glow);
            }
        }
    }

    private void GlowUpdate() {
        if (selectedOrderItemIndex >= currentOrder.GetItems().Length) return;

        foreach (ItemGlow glow in highlightObjects){
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

                    // Creating object
                    heldFlower = new GameObject();
<<<<<<< Updated upstream
<<<<<<< Updated upstream

                    // Attaching sprite
=======
                    heldFlower.tag = "MaskScene";
>>>>>>> Stashed changes
=======
                    heldFlower.tag = "MaskScene";
>>>>>>> Stashed changes
                    SpriteRenderer spriteRender = heldFlower.AddComponent<SpriteRenderer>();
                    spriteRender.sprite = attFlower.GrowingSprites[3];
                    spriteRender.sortingOrder = 5;

                    // Applying transfomations
                    heldFlower.transform.position = mouseWorld;
                    heldFlower.transform.localScale = new Vector2(4, 4);
                    heldFlower.transform.rotation = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 2 * Mathf.PI)));

                    // Adding data
                    DragFlower dragData = heldFlower.AddComponent<DragFlower>();
                    dragData.flowerId = attFlower.Name;
                    

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
                    heldFlower.transform.parent = transform;

                    if (heldFlower.GetComponent<DragFlower>().flowerId == currentOrder.GetItems()[selectedOrderItemIndex].GetName() ) {
                        placedFlowers++;
                        score += (int)(currentOrder.GetItems()[selectedOrderItemIndex].GetAccuracy(heldFlower.transform.position) * 1000f);
                        Debug.Log(currentOrder.GetItems()[selectedOrderItemIndex].GetAccuracy(heldFlower.transform.position) * 1000f);
                    }
                }

                if (!didCollide) Destroy(heldFlower);
                heldFlower = null; // Using for resetting + safety

                debounce = false;
            }
        }
    }

    private void CompletedDecorCheck() {
        if (placedFlowers >= currentOrder.GetItems()[selectedOrderItemIndex].GetAmount()) {
            placedFlowers = 0;
            selectedOrderItemIndex++;
            PlaceHighlights();

            // Collect score

            if (selectedOrderItemIndex >= currentOrder.GetItems().Length) {
                // Finish mask decorating
                Debug.Log("FINISHED!");
            }
        }
    }


    public void DecorateTick() {
        GlowUpdate();
        FlowerPotUpdate();
        CompletedDecorCheck();
    }
}
