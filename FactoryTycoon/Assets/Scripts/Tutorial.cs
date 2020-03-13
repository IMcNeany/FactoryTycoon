using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject production;
    public GameObject Disposal;
    public GameObject UI;
    public List<GameObject> panels;
    GenerateGrid grid;
    bool fourthActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        grid = FindObjectOfType<GenerateGrid>();
        SetFactory();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(fourthActive)
        {
            if(!UI.activeSelf)
            {
                fourthActive = false;
                Turn(4);
            }
        }
    }

    void SetFactory()
    {

        //production
        GridTile gridTile = FindEmpty();
        gridTile.occupied = true;
        gridTile.itemQuantity = 80;
        //gridTile.tileUpgradeName = "RawMaterial";
        gridTile.UpgradeSection = production.GetComponent<ActiveUpgrade>().itemSection;
        // gridTile.transform.position;
       GameObject instance =  Instantiate(production.GetComponent<ActiveUpgrade>().spriteToPlace, gridTile.transform);
        instance.transform.localScale = new Vector3(1, 1, 1);
        instance.transform.localPosition = new Vector3(0, 0, -1);

        //disposal
         gridTile = FindEmpty();
        gridTile.occupied = true;
        gridTile.itemQuantity = 1;
        //gridTile.tileUpgradeName = "RawMaterial";
        gridTile.UpgradeSection = Disposal.GetComponent<ActiveUpgrade>().itemSection;
        // gridTile.transform.position;
         instance = Instantiate(Disposal.GetComponent<ActiveUpgrade>().spriteToPlace, gridTile.transform);
        instance.transform.localScale = new Vector3(1, 1, 1);
        instance.transform.localPosition = new Vector3(0, 0, -1);

        FindObjectOfType<GameManager>().UpdateMoney(-900);
    }
     void TurnOffPanels()
    {
        for(int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }
    }

   public void Turn(int i)
    {
        TurnOffPanels();
        panels[i].SetActive(true);
    }
  

    public void TutorialChecker(string stage)
    {
        if(FindObjectOfType<GameManager>().tutorialActive)
        {
            switch(stage)
            {
                case "buildPress":
                    {
                        Turn(2);
                    }
                    break;
                case "RawClicked":
                    {
                        Turn(3);
                        fourthActive = true;
                       
                    }
                    break;
                case "StartTurn":
                    {
                        Turn(6);
                    }
                    break;
                case "ContinuePressed":
                    {
                        Turn(7);
                    }
                    break;
                case "FinalPress":
                    {
                        TurnOffPanels();
                        FindObjectOfType<GameManager>().tutorialActive = false;
                    }
                    break;
            }
        }
    }


    GridTile FindEmpty()
    {

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (!tile.occupied && tile.factorySection == "Factory")
                {
                    return tile;
                }
            }
        }

        //no empty ones
        return null;
    }
}
