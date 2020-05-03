using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperHints : MonoBehaviour
{
    public GameObject turnButton;
    public GameObject buildButton;
    public GameObject mainUi;
    GameManager gameManager;
    GenerateGrid grid;
    float timer = 20;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        grid = FindObjectOfType<GenerateGrid>();
    }

    // Update is called once per frame
    void Update()
    {

        //not upgrade to place
        //main ui not open 
        //if button pressed resettimer
        if (!gameManager.tutorialActive)
        {
            if (gameManager.gameObject.GetComponent<ActiveUpgrade>().spriteToPlace == null && !mainUi.activeSelf)
            {
                time += Time.deltaTime;
            }

            if (time >= timer)
            {
                CheckButton();
            }
        }
    }
    public void ResetTimer()
    {
        time = 0;
    }

    void CheckButton()
    {
        if(CheckAllMachines())
        {
            //turn button
            turnButton.GetComponent<Pulse>().play = true;
        }
        else
        {
            //build button
            buildButton.GetComponent<Pulse>().play = true;
        }
        ResetTimer();
    }

    bool CheckAllMachines()
    {
        int production =0;
        int rawmat = 0;
        int disposal = 0;

        for(int i =0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i,j);

                if (tile.UpgradeSection == "Production")
                {
                    production++;
                }
                else if(tile.UpgradeSection == "Disposal")
                {
                    disposal++;
                }
                else if(tile.UpgradeSection == "RawMaterial")
                {
                    rawmat++;
                }
            }
        }
        if (production != 0 && disposal != 0 && rawmat != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
