using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    float TargetSocial;
    float TargetEconomical;
    float TargetEnvironmental;
    float Targetmoney;
    int totalTurns;
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
                   
                }
                break;
            case 2:
                {
                    
                }
                break;
            case 3:
                {
                  
                }
                break;
            case 4:
                {
                  
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
                    goalText = "Make £500 in 5 turns";
                    totalTurns = 5;
                    Targetmoney = 500;
                }
                break;
            case 1:
                {
                    goalText = "Not implemented yet";
                   totalTurns = 15;
                }
                break;
            case 2:
                {
                    goalText = "Not implemented yet";
                    totalTurns = 10;
                }
                break;
            case 3:
                {
                    goalText = "Not implemented yet";
                    totalTurns = 10;
                }
                break;
            case 4:
                {
                    goalText = "Not implemented yet";
                    totalTurns = 10;
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

    public bool GetTutorial()
    {
        return tutorial;
    }

    public int GetTotalTurns()
    {
        return totalTurns;
    }
}
