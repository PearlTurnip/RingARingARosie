using UnityEngine;

public class Mask : MonoBehaviour
{
    Order currentOrder;
    

    int selectedOrderItemIndex;
    int aliveTime = 0;

    ItemGlow[] highlightObjects;


    public void PlaceHighlights() {
        foreach (OrderItem item in currentOrder.GetItems() ){
            for (int i=0; i<item.GetValidPositions().Length; i++) {
                Vector2 highlightPos = item.GetValidPositions()[i];

                // Place highlighted sprite here
                GameObject itemObject = new GameObject();
                SpriteRenderer renderer = itemObject.AddComponent<SpriteRenderer>();
                itemObject.AddComponent<ItemGlow>();
                renderer.sprite = item.GetSprite();

            }
        }
    }


    private void Update(){
        foreach (ItemGlow glow in highlightObjects){
            //if (glow.GetName() == currentOrder.GetName())
            //{

            //}
        }
    }
}
