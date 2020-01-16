using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    GenerateGrid grid;
    float totalRawMat = 0;


    List<GridTile> productionTiles;
    List<GridTile> DisposalTiles;
    // Start is called before the first frame update
    void Start()
    {
        productionTiles = new List<GridTile>();
        DisposalTiles = new List<GridTile>();

         grid = FindObjectOfType<GenerateGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* count the total raw materials
     * find no of production machines
     * divide items up between machines evenly
     * items thorugh machine * productionquality (which is money made)
     * add the money made from turning raw -> product
     * check disposal machines, multiple = divide rescources amoung
     * currently all will do landfill..
     * */

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

    void UpdateStats()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateUI();
    }
}
