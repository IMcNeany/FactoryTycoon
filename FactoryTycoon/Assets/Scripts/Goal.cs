using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    float TargetSocial;
    float TargetEnvironmental;
    float Targetmoney;
    int totalTurns = -2;
    Difficulty currentDifficulty;
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
        recession,
        thrifty
    }

    public enum Scenario
    {
        normal,
        thrifty,
        recession
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
                    else if(gameManager.GetTurn() == totalTurns)
                    {
                        gameManager.Win(GameManager.GameOverCode.CompTurn);
                    }
                }
                break;
            case 3:
                {
                    if (gameManager.social < TargetSocial &&  gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailSocEco);
                    }
                    else if(gameManager.social < TargetSocial)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailTarSocial);
                    }
                    else if(gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver(GameManager.GameOverCode.FailTarEco);
                    }
                    else if(gameManager.GetTurn() == totalTurns)
                    {
                        gameManager.Win(GameManager.GameOverCode.CompTurn);
                    }
                }
                break;
            case 4:
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
        /*
         * •“Easy” : Hard to lose public approval + longer game lengths + lower volumes of waste to handle 

•“Medium” : Environmental impacts build quicker  + lower efficiencies from renewable tech 

•“Hard” : short game lengths + quickly lose approval if continue with non environmental methods 

•“Recession” : High approval from low environmental impact but public won’t spend money 

•“Thrifty” : Start with  lots of renewable raw materials but no production facilities 
*/

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
                    goalText = "Keep the social pillar above -3 for 10 turns";
                    totalTurns = 10;
                    TargetSocial = -3;
                }
                break;
            case 3:
                {
                    goalText = "Keep the environmental and social pillars above 0 for 10 turns";
                    totalTurns = 10;
                    TargetEnvironmental = 0;
                    TargetSocial = 0;
                }
                break;
            case 4:
                {
                    tutorial = true;
                    goalText = "Make £400k Profit";
                 
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
                }
                break;
            case 1:
                {
                    waste = 50;
                    startCash = 1100;
                    baseSell = 120;
                }
                break;
            case 2:
                {
                    waste = 40;
                    startCash = 1000;
                    baseSell = 100;
                }
                break;
            case 3:
                {

                    waste = 60;
                    startCash = 1500;
                    baseSell = 150;
                    tutorial = true;
                    goalText = "Make £400k Profit";

                    Targetmoney = 400;
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

    public bool GetTutorial()
    {
        return tutorial;
    }

    public int GetTotalTurns()
    {
        return totalTurns;
    }
}
