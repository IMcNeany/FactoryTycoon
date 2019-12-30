using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        { 
            CheckRayCast(Input.mousePosition);
        }
    }

    void CheckRayCast(Vector3 pos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
      
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        GameObject hitObject = hit.collider.gameObject;


        Debug.Log(hit.collider);
        //RaycastHit()
    }
}
