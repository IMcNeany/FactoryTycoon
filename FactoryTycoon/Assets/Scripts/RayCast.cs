using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCast : MonoBehaviour
{
    public GameManager gameManager;


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
            GridTile gridTile = hitObject.GetComponent<GridTile>();
            Debug.Log("grid obj");
            if(gridTile.factorySection == "Factory" && gridTile.occupied != true)
            {
                GameObject machine = Instantiate(gameManager.spriteToPlace, gridTile.transform);
                machine.transform.position = new Vector3(gridTile.transform.position.x, gridTile.transform.position.y, -1);
                machine.transform.localScale = new Vector3(4, 4, 1);
                gridTile.occupied = true;
                gameManager.spriteToPlace = null;

                //need to take away the money
                //and add stats
                
            }
        }
    }
}
