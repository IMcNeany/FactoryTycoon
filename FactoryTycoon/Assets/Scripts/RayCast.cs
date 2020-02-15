using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCast : MonoBehaviour
{
    public GameManager gameManager;
    public ActiveUpgrade activeUpgrade;


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
            Debug.Log("grid obj");
            if (activeUpgrade.itemSection == "RawMaterial")
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
            else
            {
                if (gridTile.factorySection == "Factory" && gridTile.occupied != true && activeUpgrade.spriteToPlace != null)
                {
                    PlaceUpgrade(gridTile);
                }
            }
        }
    }

    void PlaceUpgrade(GridTile gridTile)
    {

        GameObject machine = Instantiate(activeUpgrade.spriteToPlace, gridTile.transform);
        machine.transform.position = new Vector3(gridTile.transform.position.x, gridTile.transform.position.y, -1);
        machine.transform.localScale = new Vector3(4, 4, 1);
        gridTile.occupied = true;
        activeUpgrade.spriteToPlace = null;
        gridTile.tileUpgradeName = activeUpgrade.title;
        gridTile.UpgradeSection = activeUpgrade.itemSection;
        gridTile.itemQuantity = activeUpgrade.ProductionQuantity;
        gameManager.UpdateMoney(-activeUpgrade.itemCost);
        gameManager.UpdateEconomic(activeUpgrade.itemEconomical);
        gameManager.UpdateEnvironmental(activeUpgrade.itemEnvironmental);
        gameManager.UpdateSocial(activeUpgrade.itemSocial);

        StartTurn turnButton = FindObjectOfType<StartTurn>();
        turnButton.CheckTiles();

        gameManager.checkRayCast = false;
    }
}
