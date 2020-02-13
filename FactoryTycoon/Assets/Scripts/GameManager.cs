using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public float environment = 0;
    public float economic = 0;
    public float social = 0;
    public float money = 1000;
    int turn = 0;

    public float sellPrice = 100;
    public GameObject question;


    public GameObject uiUpgrades;
    public bool checkRayCast = true;
    public bool checkUIRayCast = true;

    public Upgrade activeUpgrade;
    public TextMeshProUGUI cash;
    public TextMeshProUGUI socialText;
    public TextMeshProUGUI economicText;
    public TextMeshProUGUI environmentText;
    public Slider socialSlider;
    public Slider economicSlider;
    public Slider environmentSlider;
    public TextMeshProUGUI Goals;
    public TextMeshProUGUI turnsLeft;
    public GameObject gameOverScreen;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        cash.SetText(" £" + money);
        SetObjectives();
        CalculateTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void UpdateMoney(float cashSum)
    {
        money += cashSum;
        cash.SetText(" £" + money);
    }

    public void UpdateSocial(float itemsocial)
    {
        social += itemsocial;

    }
    public void UpdateEconomic(float itemEconomical)
    {
        economic += itemEconomical;


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
        economicText.SetText(economic.ToString());
        environmentText.SetText(environment.ToString());


        socialSlider.SetValueWithoutNotify(social);
        economicSlider.SetValueWithoutNotify(economic);
        environmentSlider.SetValueWithoutNotify(environment);



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
        if (turn % 5 == 0)
        {
            question.SetActive(true);
        }
    }

    void SetObjectives()
    {
       Goal goalSetter =  FindObjectOfType<Goal>();

 
       Goals.SetText(goalSetter.goalText);
    }

   void QuitGame()
    {
        Application.Quit();
    }

    void CalculateTurn()
    {
        Goal goalSetter = FindObjectOfType<Goal>();
        int tLeft = goalSetter.GetTotalTurns() - turn;
        turnsLeft.SetText(tLeft.ToString());
        if(tLeft <= 0)
        {
            if (!winScreen.activeSelf)
            {
                GameOver();
            }
        }
  }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene(0);
    }}
