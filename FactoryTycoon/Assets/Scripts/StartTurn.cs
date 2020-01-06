using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTurn : MonoBehaviour
{
    GenerateGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<GenerateGrid>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTiles()
    {
        //GridTile[,] gridTiles = new GridTile[grid.GetHeight(), grid.GetWidth()];
        //   gridTiles = grid.GetGrid();
        int disposal = 0;
        int rawMaterial = 0;
        int production = 0;

        for(int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                if(grid.GetGrid(i,j).UpgradeSection == "Disposal")
                {
                    disposal++;
                }
                else if(grid.GetGrid(i, j).UpgradeSection == "RawMaterial")
                {
                    rawMaterial++;
                }
                else if(grid.GetGrid(i, j).UpgradeSection == "Production")
                {
                    production++;
                }
            }
        }
        if(disposal > 0 && rawMaterial > 0 && production > 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }

    }
}
