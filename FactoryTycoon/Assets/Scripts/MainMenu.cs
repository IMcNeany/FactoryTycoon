using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject difficultyCanvas;
    public GameObject consentCanvas;
    public GameObject scenario;
    // Start is called before the first frame update
    void Start()
    {
        MainMenus();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void MainMenus()
    {
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(true);
        consentCanvas.SetActive(false);
        scenario.SetActive(false);
    }

    public void OpenDifficulty()
    {
        difficultyCanvas.SetActive(true);
        playCanvas.SetActive(false);
        consentCanvas.SetActive(false);
        scenario.SetActive(false);
    }

    public void OpenConsent()
    {
#if UNITY_WEBGL
        if (!difficultyCanvas.activeSelf)
        {
            OpenDifficulty();
        }
        else
        {
            MainMenus();
        }
#else
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(false);
        consentCanvas.SetActive(true);
        scenario.SetActive(false);
#endif
    }

    public void OpenScenario(int diffNo)
    {
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(false);
        consentCanvas.SetActive(false);
        scenario.SetActive(true);
        FindObjectOfType<Goal>().SetGoal(diffNo);
    }

    public void LoadScene(int diffNo)
    {
        FindObjectOfType<Goal>().SetScenario(diffNo);
        FindObjectOfType<SceneSwitcher>().SwitchScene(1);

    }
}
