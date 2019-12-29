using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleUpgrade : Upgrades
{
    Button[] buttons;
    // Start is called before the first frame update
    protected override void Start()
    {
       
      
        CreatePanel();


        title = "Item Title";
        description = "content description of upgrade/machine" + "\n" +
            "Social: +2" + "\n" +
            "Ecomical: +3" + "\n" +
            "Enviromental: -2";
        cost = 100000;


    }

    void CreatePanel()
    {
        base.Start();
        //parent of the parent
        panel = base.GetPanelRef();
        Transform parent = this.transform.parent;
        Transform panelParent = parent.transform.parent;
        panel.transform.SetParent(panelParent);
        panel.transform.localScale = Vector3.one;
        panel.transform.localPosition = Vector3.zero;
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.offsetMax = Vector3.zero;
        panelRect.offsetMin = Vector3.zero;
    }

    void UpdatePanel()
    {
        //turn to list
        TextMeshProUGUI[] textBoxes;
           textBoxes = panel.transform.GetChild(0).transform.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < textBoxes.Length; i++)
        {
            if (textBoxes[i].gameObject.name == "Title")
            {
                textBoxes[i].SetText(title);
            }
            else if(textBoxes[i].gameObject.name == "Description")
            {
                textBoxes[i].SetText(description);
            }

        }

       // Button[] buttons;
        buttons = panel.transform.GetChild(0).transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.name == "Purchase")
            {
                TextMeshProUGUI purchaseText = buttons[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                purchaseText.SetText("Purchase for £" + cost);
            }
        }

    }

    void CheckPurchaseCost()
    {
        // get game controller with total money

        //compare cost
        //if(cost < totalgamemoney)
        //{
        //    buttons[0].interactable = true;
        //}
        //else
        //{
        //    buttons[0].interactable = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void ButtonClicked()

    {
        if(panel == null)
        {
            CreatePanel();
        }

       // panel.SetActive(false);
       // base.OnButtonClicked();
        UpdatePanel();
        CheckPurchaseCost();
        panel.SetActive(true);
    }
}
