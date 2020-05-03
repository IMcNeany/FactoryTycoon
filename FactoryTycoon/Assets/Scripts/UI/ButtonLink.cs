using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLink : MonoBehaviour
{
    public Button increase;
    public Button decrease;
    int numberofItems = 1;
    int maxNumber = 1;
    Upgrade upgrade;
    int storageFree;
    int factoryFree;
    GenerateGrid grid;
    GameManager gameManager;
    public TMPro.TextMeshProUGUI number;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GenerateGrid>();
       
        number.SetText(numberofItems.ToString());
        if (numberofItems == 1)
        {
            decrease.interactable = false;
        }
        upgrade.Setnumber(numberofItems);
    }

    public void GetupgradeLink(Upgrade link)
    {
        upgrade = link;
        if (upgrade.GetItemSection() == "RawMaterial")
        {
            CountEmptyStorage();
            if(storageFree == 0)
            {
                increase.interactable = false;
                decrease.interactable = false;
                numberofItems = 1;
                number.SetText(numberofItems.ToString());
            }
            else
            {
                if(CalculateMaxCost() > storageFree)
                {
                    maxNumber = storageFree;
                }
                else
                {
                    maxNumber = CalculateMaxCost();
                }
            
            }
        }
        else
        {
            CountEmptyFactory();
            if (factoryFree == 0)
            {
                increase.interactable = false;
                decrease.interactable = false;
                numberofItems = 1;
                number.SetText(numberofItems.ToString());
            }
            else
            {
                if (CalculateMaxCost() > factoryFree)
                {
                    maxNumber = factoryFree;
                }
                else
                {
                    maxNumber = CalculateMaxCost();
                }
                //max value of storage
                //compare cost

            }
        }
        if(maxNumber == 0)
        {
            increase.interactable = false;
            decrease.interactable = false;
        }
        if(maxNumber == 1 && numberofItems == 1)
        {

            increase.interactable = false;
            decrease.interactable = false;
        }
        else if(gameManager.tutorialActive)
        {
            numberofItems = 1;

            increase.interactable = false;
            decrease.interactable = false;
        }
    }
    public void IncreasePressed()
    {
        numberofItems++;
        number.SetText(numberofItems.ToString());
        if (numberofItems == maxNumber)
        {
            increase.interactable = false;
        }
        if (numberofItems > 1)
        {
            decrease.interactable = true;
        }
        upgrade.Setnumber(numberofItems);
    }
    
    public void DescreasedPressed()
    {
        numberofItems--;
        if (numberofItems == 1)
        {
            decrease.interactable = false;
        }
    
        number.SetText(numberofItems.ToString());
        if (numberofItems < maxNumber)
        {
            increase.interactable = true;
        }
        upgrade.Setnumber(numberofItems);
    }

    void CountEmptyStorage()
    {
        grid = FindObjectOfType<GenerateGrid>();
        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (tile.factorySection == "Storage")
                {
                    if(!tile.occupied)
                    {
                        storageFree++;
                    }
                }
            }
        }
    }

    void CountEmptyFactory()
    {
        grid = FindObjectOfType<GenerateGrid>();
        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (tile.factorySection == "Factory")
                {
                    if (!tile.occupied)
                    {
                        factoryFree++;
                    }
                }
            }
        }
    }

    int CalculateMaxCost()
    {
        gameManager = FindObjectOfType<GameManager>();
        return ((int)gameManager.money / (int)upgrade.GetCost());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
