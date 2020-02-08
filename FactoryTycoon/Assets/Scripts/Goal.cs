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

    public enum Difficulty
    {
        easy,
        medium,
        hard,
        recession,
        thrifty
    }
    // Start is called before the first frame update
    void Awake()
    {
        SetGoal(0);
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
                        Debug.Log("win");
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

    public void SetGoal(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
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
                    totalTurns = 15;
                }
                break;
            case 2:
                {
                    totalTurns = 10;
                }
                break;
            case 3:
                {
                    totalTurns = 10;
                }
                break;
            case 4:
                {
                    totalTurns = 10;
                }
                break;
        }
    }

    public int GetTotalTurns()
    {
        return totalTurns;
    }
}
