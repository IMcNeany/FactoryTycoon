using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameOverCode
    {
        Waste = 0,
        NoTurn = 1,
        GotTarMoney =2,
        FailTarEco =3,
        FailTarSocial = 4,
        FailSocEco = 5,
        CompTurn = 6,
        NoMoney = 7
    }

    public float environment = 0;
    public float social = 0;
    public float startingCost = 1000;
    public float money = 1000;
    public float profit = 0;
    public bool tutorialActive = false;
    public bool completedLevel = false;
    public bool win = false;
    public bool lose = false;
    public float wasteCount = 60;
    int turn = 0;
    public int moreInfoEngagement = 0;
    public string difficultyLevel = null;
    public string scenarioName = null;

    public float sellPrice = 100;
  //  public GameObject question;

    public GameObject wasteSelling;
    public Button wasteClose;

    public GameObject Selling;
    public Button closeSell;


    public GameObject uiUpgrades;
    public bool checkRayCast = true;
    public bool checkUIRayCast = true;

    public Upgrade activeUpgrade;
    public GameObject tutorial;

    public TextMeshProUGUI cash;
    public TextMeshProUGUI profitText;
    public TextMeshProUGUI socialText;
    public TextMeshProUGUI environmentText;

    public TextMeshProUGUI Goals;
    public TextMeshProUGUI turnsLeft;

    public TextMeshProUGUI setFirstGoalText;
    public GameObject StartText;

    public GameObject gameOverScreen;
    public TextMeshProUGUI winOrLose;
    public TextMeshProUGUI StatsText;
    public TextMeshProUGUI explanationText;
    public AudioClip winner;
    public AudioClip loser;
    public AudioSource gameoverAudio;

    // Start is called before the first frame update
    void Start()
    {
        Goal goal = FindObjectOfType<Goal>();
        gameOverScreen.SetActive(false);
        StartText.SetActive(true);
        sellPrice = goal.baseSell;
        wasteCount = goal.waste;
        money = goal.startCash;
        startingCost = goal.startCash;
        cash.SetText(" £" + money + "K");
        SetObjectives();
        CalculateTurn();
        tutorialActive = FindObjectOfType<Goal>().GetTutorial();
        difficultyLevel = FindObjectOfType<Goal>().GetDifficulty();
        scenarioName = FindObjectOfType<Goal>().GetScenario();
        
        if (tutorialActive)
        {
            //  FindObjectOfType<Tutorial>().gameObject.SetActive(true);
            tutorial.SetActive(true);
            //FindObjectOfType<Tutorial>().Turn(0);
            tutorial.GetComponent<Tutorial>().Turn(0);
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            FindObjectOfType<Analytics>().SaveStats();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void UpdateMoney(float cashSum)
    {
        money += cashSum;
        cash.SetText(" £" + money + "K");
        UpdateProfit();
    }

    void UpdateProfit()
    {
        profit = startingCost - money;
        if(profit < 0)
        {
           profit =  Mathf.Abs(profit);
            profitText.SetText("<color=green> £" + profit + "K </color>");
        }
        else
        {
         
            profitText.SetText("<color=red> -£" + profit + "K </color>");
             profit = (-profit);
        }

    }

    public void UpdateSocial(float itemsocial)
    {
        social += itemsocial;

    }

    public void UpdateEnvironmental(float itemsEnvironmental)
    {
        environment += itemsEnvironmental;

    }


    public void CloseUpgradeUI()
    {
        uiUpgrades.SetActive(false);
    }

 public void UpdateUI()
    {

        //deciding between the bars and having the text at the top one will be removed
        socialText.SetText(social.ToString());
      
        environmentText.SetText(environment.ToString());

    }
   public int GetTurn()
    {
        return turn;
    }

    public void IncreaseTurn()
    {
        turn++;
        
        Goal goalSetter = FindObjectOfType<Goal>();
        //check if player has reached objective
        goalSetter.TurnGoal();
        CalculateTurn();
      //  if (turn % 5 == 0)
       // {
        //    question.SetActive(true);
        //}
    }

    void SetObjectives()
    {
       Goal goalSetter =  FindObjectOfType<Goal>();

 
       Goals.SetText(goalSetter.goalText);
       setFirstGoalText.SetText(goalSetter.goalText + " and keep the waste below " + goalSetter.waste + "% of the storage capacity.");
    }

    public void MoreInfoClicked()
    {
        moreInfoEngagement++;
    }

   void QuitGame()
    {
        Analytics analytics = FindObjectOfType<Analytics>();
        analytics.SaveStats();
        while(analytics.statsSaved != true)
        {

        }
        Application.Quit();
  
    }

    public void OpenWaste()
    {
        if(money - 100 >= 0)
        {
            wasteClose.interactable = true;
        }
        else
        {
            wasteClose.interactable = false;
        }
        wasteSelling.SetActive(true);
    }

    public void OpenSelling()
    {
     
        closeSell.interactable = true;
       
        Selling.SetActive(true);
    }

    void CalculateTurn()
    {

        Goal goalSetter = FindObjectOfType<Goal>();
        if (goalSetter.GetTotalTurns() != -2)
        {
            int tLeft = goalSetter.GetTotalTurns() - turn;
            turnsLeft.SetText("Turns left:" + tLeft.ToString());
            if (tLeft <= 0)
            {
                if (!gameOverScreen.activeSelf)
                {
                    completedLevel = true;
                   
                    GameOver( GameOverCode.NoTurn);
                }
            }
        }
        else
        {
            turnsLeft.SetText("Turn:" + turn.ToString());
        }
  }

    public void GameOver(GameOverCode code)
    {
        CheckReason(code);
        lose = true;
        Analytics analytics = FindObjectOfType<Analytics>();
        analytics.SaveStats();
        winOrLose.SetText("You lost..");
        string turns;
        if(turn <=1)
        {
            turns = "turn.";
        }
        else
        {
            turns = "turns.";
        }

        StatsText.SetText("You made £" + profit + "K in " + turn + " " + turns + "You achieved a social rating of " + social + " and an environmental rating of " + environment + ".");
      //  gameoverAudio.PlayOneShot(loser);
        gameoverAudio.clip = loser;
        gameOverScreen.SetActive(true);
    }

    public void Win(GameOverCode code)
    {
        CheckReason(code);
        completedLevel = true;
        win = true;
        string turns;
        if (turn <= 1)
        {
            turns = "turn.";
        }
        else
        {
            turns = "turns.";
        }
        winOrLose.SetText("You Won!");
        StatsText.SetText("You made £" + profit + "K in " + turn +" " + turns + "You achieved a social rating of " + social + " and an environmental rating of " + environment + ".");
        // winScreen.SetActive(true);
        gameoverAudio.clip = winner;
        gameOverScreen.SetActive(true);

    }

    void CheckReason(GameOverCode code)
    {
        switch ((int)code)
        {
        //      Waste = 0,
        //NoTurn = 1,
        //GotTarMoney = 2,
        //FailTarEco = 3,
        //FailTarSocial = 4,
        //FailSocEco = 5,
        //CompTurn = 6
            case 0:
                {
                    explanationText.SetText("You filled up too much storage with waste! Try and use more environmentally friendly disposal methods.");
                }
                break;
            case 1:
                {
                    explanationText.SetText("You didn't meet the target with in the number of turns!");
                }
                break;
            case 2:
                {
                    explanationText.SetText("You reached the Target amount of money!");
                }
               
                break;
            case 3:
                {
                    explanationText.SetText("You created too much pollution! Try and choose more environmentally friendly options.");
                }
                break;
            case 4:
                {
                    explanationText.SetText("You failed to keep up the relationship with those around you. Try to choose options that the community around favours.");
                }
                break;
            case 5:
                {
                    explanationText.SetText("You failed to keep up social and environmental expectations! Keep an eye on those levels next time.");
                }
                break;
            case 6:
                {
                    explanationText.SetText("You reached the goal within the number of turns!");
                }
                break;
            case 7:
                {
                    explanationText.SetText("You ran out of money! You couldn't keep up with factory costs.");
                }
                break;
        }
    }

    public void LoadMainMenu()
    {
        Analytics analytics = FindObjectOfType<Analytics>();
        analytics.SaveStats();
        SceneManager.LoadScene(0);
        Destroy(FindObjectOfType<Goal>().gameObject);
    }
}
