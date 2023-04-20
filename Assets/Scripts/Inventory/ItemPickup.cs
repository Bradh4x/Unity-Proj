using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemInv Item;
    public GameObject Player;
    TextMeshProUGUI message;

    IEnumerator Pickup()
    {
        if (Mathf.Abs(Vector2.Distance(Player.transform.localPosition, this.transform.position)) <= .5f || Mathf.Abs(Vector2.Distance(Player.transform.localPosition, this.transform.position)) <= .5f)
        {
            

            InventoryMgr.Instance.Add(Item);
            InventoryMgr.Instance.ListItems();

            message = ActionManager.Instance.MessageContent.GetComponent<TextMeshProUGUI>();
            yield return new WaitUntil(() => message.text == string.Empty);

            ActionManager.Instance.MessageUI.SetActive(true);

            ActionManager.Instance.StartCoroutine("Set", $"Acquired {Item.itemName}");

            Destroy(gameObject);
            //yield return new WaitForSeconds(2);
        }
        else
        {
            message = ActionManager.Instance.MessageContent.GetComponent<TextMeshProUGUI>();
            yield return new WaitUntil(() => message.text == string.Empty);
            ActionManager.Instance.MessageUI.SetActive(true);
            ActionManager.Instance.StartCoroutine("Set", $"Too far away!");
            
        }
    }

    private void OnMouseDown()
    {
        print(Vector2.Distance(Player.transform.localPosition, this.transform.position));
        //StartCoroutine("Waiter", 1.90f);
        StartCoroutine("Pickup");
    }

    IEnumerator Waiter(float secs)
    {
        yield return new WaitForSeconds(secs);
    }


    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine("Pickup");
        }
    }
}
