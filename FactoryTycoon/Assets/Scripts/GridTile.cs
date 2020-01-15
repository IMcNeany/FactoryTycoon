using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GridTile : MonoBehaviour
{
    Vector2 position;
    Vector2 GridNode;
    public string factorySection;
    public string tileUpgradeName;
    public string UpgradeSection;
    public float itemQuantity; 
    public bool occupied = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitialiseTile( Vector2 pos, Vector2 nodePos, string section)
    {
        position = pos;
        GridNode = nodePos;
        factorySection = section;
        TintGrid();
    }

    public void ResetTile()
    {
        
        tileUpgradeName =null;
        UpgradeSection = null;
        itemQuantity = 0;
        occupied = false;
    }

    void TintGrid()
    {
        if(factorySection == "Storage")
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7305146f, 0.6032253f, 0.430728f);
            occupied = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
