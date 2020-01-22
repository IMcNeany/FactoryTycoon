using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{


    public float environment = 0;
    public float economic = 0;
    public float social = 0;
    public float money = 1000;
    int turn = 0;

    public float sellPrice = 100;
    public GameObject question;


    public GameObject uiUpgrades;
    public bool checkRayCast = true;
    public bool checkUIRayCast = true;

    public Upgrade activeUpgrade;
    public TextMeshProUGUI cash;

    // Start is called before the first frame update
    void Start()
    {
        cash.SetText(" £" + money);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMoney(float cashSum)
    {
        money += cashSum;
        cash.SetText(" £" + money);
    }

    public void UpdateSocial(float itemsocial)
    {
        social += itemsocial;

    }
    public void UpdateEconomic(float itemEconomical)
    {
        economic += itemEconomical;


    }
    public void UpdateEnvironmental(float itemsEnvironmental)
    {
        environment += itemsEnvironmental;

    }


    public void CloseUpgradeUI()
    {
        uiUpgrades.SetActive(false);
    }

    public int GetTurn()
    {
        return turn;
    }

    public void IncreaseTurn()
    {
        turn++;
        if(turn % 5 == 0)
        {
            question.SetActive(true);
        }
    }


}
