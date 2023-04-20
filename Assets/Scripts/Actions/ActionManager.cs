using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public Transform MessageContent;
    public GameObject MessageUI;

    TextMeshProUGUI MessageText;// = new TextMeshProUGUI();

    public static ActionManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //MessageText.text = string.Empty;
        //MessageText = GetComponent<TextMeshProUGUI>();
        //MessageText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Set(string msg)
    {
        //GameObject obj = Instantiate(MessageUI, MessageContent);
        MessageUI.SetActive(true);
        MessageText = MessageContent.transform.GetComponent<TextMeshProUGUI>();
        MessageText.text = msg;

        yield return new WaitForSeconds(2.0f);

        MessageUI.SetActive(false);
        MessageText.text = string.Empty;
    }
}
