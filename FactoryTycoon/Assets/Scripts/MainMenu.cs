using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject difficultyCanvas;
    public GameObject consentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(true);
        consentCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OpenDifficulty()
    {
        difficultyCanvas.SetActive(true);
        playCanvas.SetActive(false);
        consentCanvas.SetActive(false);
    }

    public void OpenConsent()
    {
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(false);
        consentCanvas.SetActive(true);
    }

    public void LoadScene(int diffNo)
    {
        FindObjectOfType<Goal>().SetGoal(diffNo);
        FindObjectOfType<SceneSwitcher>().SwitchScene(1);

    }
}
