using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCast : MonoBehaviour
{
    public GameManager gameManager;
    public ActiveUpgrade activeUpgrade;
    GridTile CurrentWasteGameTile = null;
    GridTile CurrentSellingGameTile = null;
    GenerateGrid grid;

    // Start is called before the first frame update
    void Start()
    {
       
   
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.checkRayCast == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                CheckRayCast(Input.mousePosition);
            }
        }
        
    }

    void CheckRayCast(Vector3 pos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
      
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit != false)
        {
            GameObject hitObject = hit.collider.gameObject;


            CheckObject(hitObject);
        }


        Debug.Log(hit.collider);
        //RaycastHit()
    }

    void CheckObject(GameObject hitObject)
    {
        Debug.Log("obj" + hitObject.name);
        if (hitObject == null)
        {
            return;
        }

        if(hitObject.GetComponent<GridTile>())
        {
            //Will check if upgrade has been selected and place it
            GridTile gridTile = hitObject.GetComponent<GridTile>();

             if (gridTile.occupied == true && gridTile.UpgradeSection == "Waste")
             {
                gameManager.OpenWaste();
                CurrentWasteGameTile = gridTile;

             }
             else if (gridTile.occupied == true && gridTile.UpgradeSection == "Production" || gridTile.occupied == true && gridTile.UpgradeSection == "Disposal")
            {
                gameManager.OpenSelling();
                CurrentSellingGameTile = gridTile;
            }
            else if (activeUpgrade.itemSection == "RawMaterial")
            {
                if (gridTile.factorySection == "Storage" && gridTile.occupied != true && activeUpgrade.spriteToPlace != null)
                {
                    PlaceUpgrade(gridTile);
                    if(gameManager.tutorialActive)
                    {
                        FindObjectOfType<Tutorial>().Turn(5);
                    }
                }
            }
            else if (gridTile.factorySection == "Factory" && gridTile.occupied != true && activeUpgrade.spriteToPlace != null)
            {
                    PlaceUpgrade(gridTile);
            }
         

            
        }
    }

    public void SellMachine()
    {
        CurrentSellingGameTile.occupied = false;
        CurrentSellingGameTile.tileUpgradeName = "";
        CurrentSellingGameTile.UpgradeSection = "";
        CurrentSellingGameTile.itemQuantity = 0;
        activeUpgrade.spriteToPlace = null;
        Destroy(CurrentSellingGameTile.transform.GetChild(0).gameObject);
        gameManager.UpdateMoney(200);
    }

    public void SellWaste()
    {
        CurrentWasteGameTile.occupied = false;
        CurrentWasteGameTile.tileUpgradeName = "";
        CurrentWasteGameTile.UpgradeSection = "";
        CurrentWasteGameTile.itemQuantity = 0;
        activeUpgrade.spriteToPlace = null;
        Destroy(CurrentWasteGameTile.transform.GetChild(0).gameObject);
        gameManager.UpdateMoney(-100);
    }

    void PlaceUpgrade(GridTile gridTile)
    {

        GameObject machine = Instantiate(activeUpgrade.spriteToPlace, gridTile.transform);
        machine.transform.position = new Vector3(gridTile.transform.position.x, gridTile.transform.position.y, -1);
        machine.transform.localScale = new Vector3(0.08f, 0.08f, 1);
        gridTile.occupied = true;
       
        gridTile.tileUpgradeName = activeUpgrade.title;
        gridTile.UpgradeSection = activeUpgrade.itemSection;
        gridTile.itemQuantity = activeUpgrade.ProductionQuantity;
        gameManager.UpdateMoney(-activeUpgrade.itemCost);
        gameManager.UpdateEnvironmental(activeUpgrade.itemEnvironmental);
        gameManager.UpdateSocial(activeUpgrade.itemSocial);

        if (activeUpgrade.number > 1)
        {
            //find random tiles for the rest
            for (int i = 1; i < activeUpgrade.number; i++)
            {
                GridTile tile = FindTile();
                machine = Instantiate(activeUpgrade.spriteToPlace, tile.transform);
                machine.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, -1);
                machine.transform.localScale = new Vector3(0.08f, 0.08f, 1);
                tile.occupied = true;

                tile.tileUpgradeName = activeUpgrade.title;
                tile.UpgradeSection = activeUpgrade.itemSection;
                tile.itemQuantity = activeUpgrade.ProductionQuantity;
                gameManager.UpdateMoney(-activeUpgrade.itemCost);
                gameManager.UpdateEnvironmental(activeUpgrade.itemEnvironmental);
                gameManager.UpdateSocial(activeUpgrade.itemSocial);
            }
        }

        activeUpgrade.spriteToPlace = null;
        gameManager.GetComponent<AudioSource>().Play();
        StartTurn turnButton = FindObjectOfType<StartTurn>();
        turnButton.CheckTiles();

        gameManager.checkRayCast = true;
    }

    GridTile FindTile()
    {
        grid = FindObjectOfType<GenerateGrid>();
        if (activeUpgrade.itemSection == "RawMaterial")
        {
            for (int i = 0; i < grid.GetHeight(); i++)
            {
                for (int j = 0; j < grid.GetWidth(); j++)
                {
                    GridTile tile = grid.GetGrid(i, j);
                    if (tile.factorySection == "Storage")
                    {
                        if(tile.occupied == false)
                        {

                            return tile;
                        }
                    }
                }
            }
        }
        else if (activeUpgrade.itemSection == "Production" || activeUpgrade.itemSection == "Disposal")
        {
            for (int i = 0; i < grid.GetHeight(); i++)
            {
                for (int j = 0; j < grid.GetWidth(); j++)
                {
                    GridTile tile = grid.GetGrid(i, j);
                    if (tile.factorySection == "Factory")
                    {
                        if (tile.occupied == false)
                        {
                            return tile;

                        }
                    }
                }
            }
        }
        return null;
    }
}
