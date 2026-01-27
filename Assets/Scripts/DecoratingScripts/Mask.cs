using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    private Order currentOrder;
    

    private int selectedOrderItemIndex;

    private List<ItemGlow> highlightObjects = new List<ItemGlow>();


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


    private void Update(){
        foreach (ItemGlow glow in highlightObjects){
            if (glow.targetName == currentOrder.GetItems()[selectedOrderItemIndex].GetName()){
                glow.UseGlow();
            }
        }
    }
}
