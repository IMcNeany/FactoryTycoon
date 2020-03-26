using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    int storageRows = 3;
    int factoryHeight = 7; public int GetHeight() { return factoryHeight; }
    int factoryWidth = 6; public int GetWidth() { return factoryWidth; }
    float squareSize = 1;
   public GameObject gridSquare;
    GridTile[,] gridTiles;

    // Start is called before the first frame update
    void Start()
    {
        gridTiles = new GridTile[factoryHeight,factoryWidth];
       // gridSquare = Resources.Load("Grid") as GameObject;

        CreateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid()
    {
        for(int i = 0; i < factoryHeight; i++)
        {
            for(int j = 0; j < factoryWidth; j++)
            {
                float xpos = this.transform.position.x + (squareSize * j);
                float ypos = this.transform.position.y + (squareSize * i);
                GameObject grid = Instantiate(gridSquare, new Vector3(xpos, ypos, 0), new Quaternion(0, 0, 0, 0));
                grid.transform.parent = this.gameObject.transform;
                grid.AddComponent<GridTile>();
                GridTile tile = grid.GetComponent<GridTile>();
                gridTiles[i,j] = tile;
                tile.InitialiseTile(new Vector2(xpos, ypos), new Vector2(i, j), TileType(i, j));
            }
            
        }
    }

    public GridTile GetGrid(int i , int j)
    {
        return  gridTiles[i,j];
    }

    string TileType(int i, int j)
    {
       int storageStart =  factoryHeight - storageRows;

        if(i >= storageStart)
        {
            return "Storage";
        }
        else
        {
            return "Factory";
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
