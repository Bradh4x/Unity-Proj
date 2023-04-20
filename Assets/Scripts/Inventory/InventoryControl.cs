using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    public GameObject Inventory;
    public Button btnInventory;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.SetActive(isOpen);
        btnInventory.onClick.AddListener(ToggleInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            ToggleInventory();  
        }
        
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        InventoryMgr.Instance.ListItems();
        Inventory.SetActive(isOpen);
    }
}
