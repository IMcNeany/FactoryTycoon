using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    float TargetSocial;
    float TargetEconomical;
    float TargetEnvironmental;
    float Targetmoney;
    int totalTurns = -2;
    Difficulty currentDifficulty;
    public string goalText;
    public bool tutorial = true;

    public enum Difficulty
    {
        easy,
        medium,
        hard,
        recession,
        thrifty
    }
    // Start is called before the first frame update
    void start()
    {
       
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
                   
                   if(gameManager.money >= Targetmoney)
                    {
                        gameManager.Win();
                    }
                }
                break;
            case 1:
                {
                   if(gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver();
                    }
                   else if(gameManager.money >= Targetmoney)
                    {
                        gameManager.Win();
                    }
                }
                break;
            case 2:
                {
                    if(gameManager.social < TargetSocial)
                    {
                        gameManager.GameOver();
                    }
                    else if(gameManager.GetTurn() >= totalTurns)
                    {
                        gameManager.Win();
                    }
                }
                break;
            case 3:
                {
                    if (gameManager.social < TargetSocial ||  gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver();
                    }
                    else if(gameManager.GetTurn() >= totalTurns)
                    {
                        gameManager.Win();
                    }
                }
                break;
            case 4:
                {
                    if (gameManager.social < TargetSocial || gameManager.economic < TargetEconomical || gameManager.environment < TargetEnvironmental)
                    {
                        gameManager.GameOver();
                    }
                    else if(gameManager.GetTurn() >= 20 && gameManager.money >= Targetmoney)
                    {
                        gameManager.Win();
                    }
                    else if(gameManager.GetTurn() > 20)
                    {
                        gameManager.GameOver();
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
                    goalText = "Make £500 profit";
                    //totalTurns = 5;
                    Targetmoney = 500;
                }
                break;
            case 1:
                {
                    goalText = "Make £1000 profit while keeping the environmental pillar above -5";
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
                    goalText = "Be a perfect buisness, Keep all three pillars above 0 for 20 turns while making a profit of over 3000";
                    totalTurns = 20;
                    TargetSocial = 0;
                    TargetEconomical = 0;
                    TargetEnvironmental = 0;
                    Targetmoney = 3000;
                    break;
                }
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
