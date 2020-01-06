using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Upgrades
{
    Button[] buttons;
    GameManager gameManager;
    // Start is called before the first frame update
    protected override void Start()
    {
        CreatePanel();

        gameManager = GameObject.FindObjectOfType<GameManager>();
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
                textBoxes[i].SetText(description + "\n" + "Social: " + social +"\n" + "Economical: " + economical + "\n" + "Environmental: " + environmental);
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

    public void SetDesc(string upgradeName, string desc, float money, float itemSocial, float itemEco, float itemEnvironmental, GameObject tile, string SecName)
    {
        title = upgradeName;
        description = desc;
        cost = money;
        social = itemSocial;
        economical = itemEco;
        environmental = itemEnvironmental;
        tileSprite = tile;
        itemUpgradeSection = SecName;
    }

    void CheckPurchaseCost()
    {
        // get game controller with total money

        //compare cost
        if (cost <= gameManager.money)
        {
            buttons[1].interactable = true;
            gameManager.checkRayCast = false;
            gameManager.checkUIRayCast = true;
            gameManager.activeUpgrade = this;
        }
        else
        {
            buttons[1].interactable = false;
            gameManager.checkRayCast = false;
            gameManager.checkUIRayCast = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceUpgrade()
    {
        gameManager.checkUIRayCast = false;
        gameManager.checkRayCast = true;

        //pass all infor to active upgrades
        ActiveUpgrade activeUpgrade = GameObject.FindObjectOfType<ActiveUpgrade>();

        activeUpgrade.title = title;
        activeUpgrade.itemSection = itemUpgradeSection;
        activeUpgrade.itemEconomical = economical;
        activeUpgrade.itemEnvironmental = environmental;
        activeUpgrade.itemSocial = social;
        activeUpgrade.itemCost = cost;
        activeUpgrade.spriteToPlace = tileSprite;


        gameManager.CloseUpgradeUI();
        Destroy(panel);

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
