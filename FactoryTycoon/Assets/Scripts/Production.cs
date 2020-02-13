using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public GameObject waste;
    public GameObject rawMat;
    public float wastePercent = 60.0f;
    GenerateGrid grid;
    float totalRawMat = 0;


    List<GridTile> productionTiles;
    List<GridTile> DisposalTiles;
    List<GridTile> StorageTiles;
    List<GridTile> WasteTiles;
    // Start is called before the first frame update
    void Start()
    {
        productionTiles = new List<GridTile>();
        DisposalTiles = new List<GridTile>();
        StorageTiles = new List<GridTile>();
        WasteTiles = new List<GridTile>();
        grid = FindObjectOfType<GenerateGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseMaterials()
    {

        UpdateStats();
        totalRawMat = 0;
        DisposalTiles.Clear();
        productionTiles.Clear();
       


        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for(int j = 0; j < grid.GetWidth();j++)
            {
               GridTile tile =  grid.GetGrid(i, j);
                if (tile.factorySection == "Storage")
                {

                    if (CountRawMats(tile))
                    {
                        tile.ResetTile();

                        Debug.Log(tile.gameObject.transform.GetChild(0).name);
                        Destroy(tile.gameObject.transform.GetChild(0).gameObject);
                    }
                
                }

                if(tile.factorySection == "Factory")
                {
                    if(tile.UpgradeSection == "Production")
                    {
                        productionTiles.Add(tile);
                    }
                    else if(tile.UpgradeSection == "Disposal")
                    {
                        DisposalTiles.Add(tile);
                    }
                }
            }
        }

        UseProductionMachines();
        UseDisposal();
        CheckWasteStorage();
        IncreaseTurnCount();


    }
    bool CountWaste(GridTile tile)
    {

        if (tile.UpgradeSection == "Waste")
        {
            return true;
        }
        return false;
    }


    bool CountRawMats(GridTile tile)
    {
        
        if(tile.UpgradeSection == "RawMaterial")
        {
            totalRawMat += tile.itemQuantity;
            return true;
        }
        return false;
    }

    void UseProductionMachines()
    {
        int perMachine = Mathf.FloorToInt( totalRawMat / productionTiles.Count) ;
        int remainder = (int)totalRawMat % (int)productionTiles.Count;
        Debug.Log(perMachine + "total " + totalRawMat + " " + productionTiles.Count + " " + remainder);
        float totalSales = 0;
        for (int i = 0; i < productionTiles.Count; i++)
        {
           totalSales += productionTiles[i].itemQuantity * perMachine;
        }
        if(remainder == 1)
        {
            totalSales += productionTiles[0].itemQuantity * remainder;
        }
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateMoney(totalSales);
    }

    void UseDisposal()
    {
         int perMachine = Mathf.FloorToInt(totalRawMat / DisposalTiles.Count);
         int remainder = (int)totalRawMat % (int)DisposalTiles.Count;

        //add up percentages on disposal machines
       
        for (int i = 0; i < DisposalTiles.Count; i++)
        {
            //calculated the percentage chance of raw material
            for (int j = 0; j < perMachine; j++)
            {
                float percentageChance = (DisposalTiles[i].itemQuantity / 100);

                if(Random.value > percentageChance)
                {
                    //waste
                    GridTile gridTile = FindEmpty();
                    gridTile.occupied = true;
                    gridTile.itemQuantity = 1;
                    gridTile.UpgradeSection = "Waste";
                    Instantiate(waste, gridTile.transform);
                }
                else
                {
                    //raw material
                    GridTile gridTile = FindEmpty();
                    gridTile.occupied = true;
                    gridTile.itemQuantity = 1;
                    //gridTile.tileUpgradeName = "RawMaterial";
                    gridTile.UpgradeSection = "RawMaterial";
                    // gridTile.transform.position;
                    Instantiate(rawMat, gridTile.transform);

                }


            }

        }
        if (remainder == 1)
        {
            float percentageChance = (DisposalTiles[0].itemQuantity / 100);

            if (Random.value > percentageChance)
            {
                //waste
                GridTile gridTile = FindEmpty();
                gridTile.occupied = true;
                gridTile.itemQuantity = 1;
                gridTile.UpgradeSection = "Waste";
                Instantiate(waste, gridTile.transform);
            }
            else
            {
                //raw material
                GridTile gridTile = FindEmpty();
                gridTile.occupied = true;
                gridTile.itemQuantity = 1;
                //gridTile.tileUpgradeName = "RawMaterial";
                gridTile.UpgradeSection = "RawMaterial";
                // gridTile.transform.position;
                Instantiate(rawMat, gridTile.transform);

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
                if (!tile.occupied && tile.factorySection == "Storage")
                {
                    return tile;
                }
            }
        }

        //no empty ones
        return null;
    }
  void UpdateStats()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateUI();
    } public void IncreaseTurnCount()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.IncreaseTurn();
    }

    void CheckWasteStorage()
    {
        StorageTiles.Clear();
        WasteTiles.Clear();

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (tile.factorySection == "Storage")
                {
                    StorageTiles.Add(tile);
                    if (CountWaste(tile))
                    {
                        WasteTiles.Add(tile);
                    }
                }
            }
        }
        float tiles = (float)WasteTiles.Count / (float)StorageTiles.Count;

        if ((tiles* 100)>= wastePercent)
        {
            Debug.Log("Game over");
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();
        }
    }
}
