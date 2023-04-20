using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMgr : MonoBehaviour
{
    bool pickedUp = false;

    public static InventoryMgr Instance;
    public List<ItemInv> Items = new List<ItemInv>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(ItemInv item)
    {
        Items.Add(item);
    }
    public void Remove(ItemInv item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (ItemInv item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            //var icon = obj.transform.GetChildCount();
            var itemIcon = obj.transform.Find("Icon").GetComponent<Image>();
            //var itemQty = obj.transform.Find("Count").GetComponent<Text>();

            //int q = int.Parse(itemQty.text);

            itemIcon.sprite = item.icon;
            //itemQty.text = "1";
        }
        for (int i = 0; i < 19 - (Items.Count - 1); i++)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
        }
    }
}



