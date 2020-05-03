using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Upgrades
{
    Button[] buttons;
    GameManager gameManager;
    GameObject copyPanel;
    // Start is called before the first frame update
    protected override void Start()
    {
        CreatePanel();

        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void CreatePanel()
    {
        //base.Start();
        //parent of the parent
        copyPanel = Instantiate( base.GetPanelRef());
      
      
        Transform parent = this.transform.parent;
        Transform panelParent = parent.transform.parent;
        Transform panelParentParent = panelParent.transform.parent;
        // panel = Instantiate(base.GetPanelRef(),panelParentParent);
        copyPanel.transform.SetParent(panelParentParent);
        copyPanel.transform.localScale = Vector3.one;
        copyPanel.transform.localPosition = Vector3.zero;
        RectTransform panelRect = copyPanel.GetComponent<RectTransform>();
        panelRect.offsetMax = new Vector3(0.0f, -20.0f, 0.0f);

        panelRect.offsetMin = Vector3.zero;
      
        copyPanel.SetActive(false);
    }

    void UpdatePanel()
    {
        //turn to list
        TextMeshProUGUI[] textBoxes;
           textBoxes = copyPanel.transform.GetChild(0).transform.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < textBoxes.Length; i++)
        {
            if (textBoxes[i].gameObject.name == "Title")
            {
                textBoxes[i].SetText(title);
            }
            else if(textBoxes[i].gameObject.name == "Description")
            {
                if (itemUpgradeSection == "Production")
                {

                    textBoxes[i].SetText(description + "\n" + "Social: " + social + "\n" + "Environmental: " + environmental + "\n" + "Recycling Percent: " + productionQuantity + "%");
                }
                else
                {
                    textBoxes[i].SetText(description + "\n" + "Social: " + social + "\n" + "Environmental: " + environmental);
                }
               
            }

        }

        TextMeshProUGUI altdescPanel = copyPanel.transform.GetChild(1).transform.GetComponentInChildren<TextMeshProUGUI>();
        if(altdescPanel.gameObject.name == "altDesc")
        {
            altdescPanel.SetText(altDescription);
        }

       // Button[] buttons;
        buttons = copyPanel.transform.GetChild(0).transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.name == "Purchase")
            {
                TextMeshProUGUI purchaseText = buttons[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                purchaseText.SetText("Purchase for £" + cost + "K");
            }
        }

        TextMeshProUGUI[] predicted = copyPanel.transform.GetChild(2).transform.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < predicted.Length; i++)
        {
            if (predicted[i].name == "Stats")
            {
                predicted[i].SetText("Cash: £" + (gameManager.money - cost * upgradeNumber) + "K" + "\n" + "Social: " + (gameManager.social + (social * upgradeNumber)) + "\n" + "Environmental: " + (gameManager.environment + (environmental*upgradeNumber)) + "\n");
            }

        }
        ButtonLink calcProductNo = copyPanel.transform.GetChild(0).transform.GetChild(5).GetComponent<ButtonLink>();
        calcProductNo.GetupgradeLink(this);
    }

    public void SetDesc(string upgradeName, string desc, float money, float itemSocial, float itemEnvironmental, GameObject tile, string SecName, float quantity, string altDesc)
    {
        title = upgradeName;
        description = desc;
        altDescription = altDesc;
        cost = money;
        social = itemSocial;
        environmental = itemEnvironmental;
        tileSprite = tile;
        itemUpgradeSection = SecName;
        productionQuantity = quantity;

    }

    public void Setnumber(int no)
    {
        upgradeNumber = no;
        UpdateStats();
    }

    void UpdateStats()
    {
        TextMeshProUGUI[] predicted = copyPanel.transform.GetChild(2).transform.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < predicted.Length; i++)
        {
            if (predicted[i].name == "Stats")
            {
                predicted[i].SetText("Cash: £" + (gameManager.money - cost * upgradeNumber) + "K" + "\n" + "Social: " + (gameManager.social + (social * upgradeNumber)) + "\n" + "Environmental: " + (gameManager.environment + (environmental * upgradeNumber)) + "\n");
            }

        }

        // Button[] buttons;
        buttons = copyPanel.transform.GetChild(0).transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.name == "Purchase")
            {
                TextMeshProUGUI purchaseText = buttons[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                purchaseText.SetText("Purchase for £" + (cost * upgradeNumber) + "K");
            }
        }
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
    public string GetItemSection()
    {
        return itemUpgradeSection;
    }

    public float GetCost()
    {
        return cost;
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
        ActiveUpgrade activeUpgrade = FindObjectOfType<GameManager>().GetComponent<ActiveUpgrade>();

        activeUpgrade.title = title;
        activeUpgrade.itemSection = itemUpgradeSection;
        activeUpgrade.itemEnvironmental = environmental;
        activeUpgrade.itemSocial = social;
        activeUpgrade.itemCost = cost;
        activeUpgrade.spriteToPlace = tileSprite;
        activeUpgrade.ProductionQuantity = productionQuantity;
        activeUpgrade.number = upgradeNumber;


        gameManager.CloseUpgradeUI();
        Destroy(copyPanel);

    }

     public void ButtonClicked()

    {
        if(copyPanel == null)
        {
            CreatePanel();
        }

       // panel.SetActive(false);
       // base.OnButtonClicked();
        UpdatePanel();
        CheckPurchaseCost();
        copyPanel.SetActive(true);
    }
}
