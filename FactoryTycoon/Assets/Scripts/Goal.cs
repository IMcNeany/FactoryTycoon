using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    float TargetSocial;
    float TargetEnvironmental;
    float Targetmoney;
    int totalTurns = -2;
    public int gridWidth = 6;
    public int gridHeight = 7;
    public int storageRows = 3;
    Difficulty currentDifficulty;
    Scenario currentScenario;
    public string goalText;
    public float waste = 60;
    public float startCash = 1000;
    public float baseSell = 150;
    public bool tutorial = false;

    public enum Difficulty
    {
        easy,
        medium,
        hard,
        tutorial
    }

    public enum Scenario
    {
        normal,
        thrifty,
        recession,
        tutorial
    }
    // Start is called before the first frame update
    void start()
    {
        //  Debug.Log(tutorial);
        tutorial = false;
        SetGoal(0);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnGoal()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        switch((int)currentDifficulty)
        {
            case 0:
                {
                    if (gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailTarEco);
                    }
                    else if (gameManager.profit >= Targetmoney)
                    {
                        gameManager.Win(GameManager.GameOverCode.GotTarMoney);
                    }
                  
                }
                break;
            case 1:
                {
                   if(gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailTarEco);
                    }
                   else if(gameManager.profit >= Targetmoney)
                    {
                        gameManager.Win(GameManager.GameOverCode.GotTarMoney);
                    }
                }
                break;
            case 2:
                {
                    if(gameManager.social < TargetSocial)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailTarSocial);
                    }
                    else if(gameManager.GetTurn() == totalTurns && gameManager.profit < Targetmoney)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.NoTurn);
                    }
                    else if(gameManager.GetTurn() == totalTurns && gameManager.profit >= Targetmoney)
                    {
                        gameManager.Win(GameManager.GameOverCode.CompTurn);
                    }
                }
                break;
      
            case 3:
                {
                    
                    if(gameManager.profit >= Targetmoney)
                    {
                        gameManager.Win(GameManager.GameOverCode.GotTarMoney);
                    }
                  
                }
                break;
        }
    }

    public void SetGoal(int difficulty)
    {
        currentDifficulty = (Goal.Difficulty) difficulty;
     

        switch((int) difficulty)
        {
            case 0:
                {
                    goalText = "Make £1000K profit while keeping the environmental pillar above -10";
                    //totalTurns = 5;
                    TargetEnvironmental = -10;
                    Targetmoney = 1000;
                }
                break;
            case 1:
                {
                    goalText = "Make £1000K profit while keeping the environmental pillar above -5";
                    // totalTurns = 15;
                    TargetEnvironmental = -5;
                    Targetmoney = 1000;
                }
                break;
            case 2:
                {
                    goalText = "Keep the social pillar above -3 for 10 turns and break-even";
                    totalTurns = 10;
                    Targetmoney = 0;
                    TargetSocial = -3;
                }
                break;
        
            case 3:
                {
                    tutorial = true;
                    goalText = "Make £400k Profit";
                    currentDifficulty = Difficulty.tutorial;
                    Targetmoney = 400;
                    break;
                }
        }

        
    }

    public void SetScenario(int scenario)
    {
        switch (scenario)
        {
            case 0:
                {
                    waste = 60;
                    startCash = 1500;
                    baseSell = 200;
                    gridHeight = 7;
                    storageRows = 3;
                    gridWidth = 6;
                    currentScenario = Scenario.normal;
                }
                break;
            case 1:
                {
                    waste = 50;
                    startCash = 1100;
                    baseSell = 120;
                    gridHeight = 6;
                    storageRows = 3;
                    gridWidth = 6;
                    currentScenario = Scenario.recession;
                }
                break;
            case 2:
                {
                    waste = 70;
                    startCash = 1000;
                    baseSell = 100;
                    gridHeight = 6;
                    storageRows = 2;
                    gridWidth = 6;
                    currentScenario = Scenario.thrifty;
                }
                break;
            case 4:
                {

                    waste = 60;
                    startCash = 1500;
                    baseSell = 150;
                    tutorial = true;
                    goalText = "Make £400k Profit";
                    currentDifficulty = Difficulty.tutorial;
                    Targetmoney = 400;
                    gridHeight = 7;
                    gridWidth = 6;
                    currentScenario = Scenario.tutorial;
                }
                break;
        }
    }
    public void ToggleTutorial()
    {
        if(tutorial)
        {
            tutorial = false;
        }
        else
        {
            tutorial = true;
        }
    }

    public string GetDifficulty()
    {
        return currentDifficulty.ToString();
    }

    public string GetScenario()
    {
        return currentScenario.ToString();
    }

    public bool GetTutorial()
    {
        return tutorial;
    }

    public int GetTotalTurns()
    {
        return totalTurns;
    }
}
