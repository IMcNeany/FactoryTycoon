using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    int storageRows = 3;
    int factoryHeight = 7; public int GetHeight() { return factoryHeight; }
    int factoryWidth = 6; public int GetWidth() { return factoryWidth; }
    float posX = 0;
    float posY = 0;
    float squareSize = 10f;
   public GameObject gridSquareFactory;
    public GameObject gridSquareStorage;
    int storageStart = 0;
    GridTile[,] gridTiles;

    // Start is called before the first frame update
    void Start()
    {
        GetGridSize();
        gridTiles = new GridTile[factoryHeight,factoryWidth];
        // gridSquare = Resources.Load("Grid") as GameObject;
       storageStart = factoryHeight - storageRows;
        CreateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetGridSize()
    {
        Goal goal = FindObjectOfType<Goal>();
        factoryWidth = goal.gridWidth;
        factoryHeight = goal.gridHeight;
        storageRows = goal.storageRows;
    }

    void CreateGrid()
    {
        for(int i = 0; i < factoryHeight; i++)
        {
            for(int j = 0; j < factoryWidth; j++)
            {
                if(i >= storageStart)
                {
                    //storage
                    SpawnTile("Storage", i, j, gridSquareStorage);
                }
                else
                {
                    //factory
                    SpawnTile("Factory", i, j, gridSquareFactory);
                }
               /* float xpos = this.transform.position.x + (squareSize * j);
                float ypos = this.transform.position.y + (squareSize * i);
                GameObject grid = Instantiate(gridSquareFactory, new Vector3(xpos, ypos, 0), new Quaternion(0, 0, 0, 0));
                grid.transform.parent = this.gameObject.transform;
                grid.AddComponent<GridTile>();
                GridTile tile = grid.GetComponent<GridTile>();
                gridTiles[i,j] = tile;
                tile.InitialiseTile(new Vector2(xpos, ypos), new Vector2(i, j), TileType(i, j));*/
            }
            
        }
    }

    void SpawnTile(string type, int i, int j, GameObject gameTile)
    {
        if(j == 0)
        {
            posX = this.transform.position.x + 0f;
        }
        else
        {
            posX = this.transform.position.x + (0.9f * j);
        }

        if(i == 0)
        {
            posY = this.transform.position.y + 0f;
        }
        else
        {
            posY = this.transform.position.y + (0.9f * i);
        }

        GameObject grid = Instantiate(gameTile, new Vector3(posX, posY, 0), new Quaternion(0, 0, 0, 0));
        grid.transform.parent = this.gameObject.transform;
        grid.AddComponent<GridTile>();
        GridTile tile = grid.GetComponent<GridTile>();
        gridTiles[i, j] = tile;
        tile.InitialiseTile(new Vector2(posX, posY), new Vector2(i, j), TileType(i, j));
    }

    public GridTile GetGrid(int i , int j)
    {
        return  gridTiles[i,j];
    }

    string TileType(int i, int j)
    {
       //int storageStart =  factoryHeight - storageRows;

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
