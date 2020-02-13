using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject difficultyCanvas;
    // Start is called before the first frame update
    void Start()
    {
        difficultyCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDifficulty()
    {
        difficultyCanvas.SetActive(true);
        playCanvas.SetActive(false);
    }

    public void LoadScene(int diffNo)
    {
        FindObjectOfType<Goal>().SetGoal(diffNo);
        FindObjectOfType<SceneSwitcher>().SwitchScene(1);

    }
}
