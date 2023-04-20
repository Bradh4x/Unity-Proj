using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public GameObject Player;
    // public ItemInv Item;
    public Transform customCursor;

    void Drop(int idx)
    {
        for (int i = 0; i < InventoryMgr.Instance.Items.Count - 1; i++)
        {
            if (InventoryMgr.Instance.Items[i].id == idx)
            {
                
                InventoryMgr.Instance.Remove(InventoryMgr.Instance.Items[i]);
               
                break;
            }
        }

        InventoryMgr.Instance.ListItems();
        

    }

    private void Update()
    {
        OnDelete();
    }


    private void OnDelete()
    { 
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0) && Vector2.Distance(customCursor.position, InventoryMgr.Instance.InventoryItem.GetComponent<Transform>().position) == 0f)
        {

            var icon = this.transform.Find("Icon").GetComponent<Image>();
            
            foreach (ItemInv item in InventoryMgr.Instance.Items)
            {
                if (icon.sprite.ToShortString().ToLower() == item.icon.name.ToLower())
                {
                    InventoryMgr.Instance.Remove(item);
                    InventoryMgr.Instance.ListItems();
                    

                    return;
                }
            }
            
            //InventoryManager.Instance.InventoryData.inventoryData.Remove()
        }
        //print(Vector2.Distance(customCursor.position, InventoryMgr.Instance.InventoryItem.GetComponent<Transform>().position));
        
    }
}
