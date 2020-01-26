using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEditor.UI;

[System.Serializable]
public class Questions
{
    public string Question;
    public string[] Answers;
    public string CorrectAnswer;
    public string CorrectExplanation;
    public string IncorrectExplanation;
}

[System.Serializable]
public class Rootobject
{
    public List<Questions> jsonObject;
}

public class JSONReader : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI answer1Text;
    public TextMeshProUGUI answer2Text;
    public TextMeshProUGUI answer3Text;

    public GameObject continueButton;

    public Rootobject loadedData;
    public List<int> QuestionsAsked;

    int newNumber;

    public void Start()
    {
        QuestionsAsked = new List<int>();
        LoadJSON();
    }

    public void LoadJSON()
    {
        loadedData = new Rootobject();
        string filePath = Path.Combine(Application.streamingAssetsPath, "Questions.Json");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            loadedData = JsonUtility.FromJson<Rootobject>("{\"jsonObject\":" + dataAsJson + "}");
            
        }
        else
        {
            Debug.Log("Error to open JSON");
        }
        SetQuestion();
    }

    public void OnEnable()
    {
        EnableAnswers();
        SetQuestion();
    }

    void EnableAnswers()
    {
        answer1Text.transform.parent.gameObject.SetActive(true);
        answer2Text.transform.parent.gameObject.SetActive(true);
        answer3Text.transform.parent.gameObject.SetActive(true);
        continueButton.SetActive(false);
    }

    void DisableAnswers()
    {
        answer1Text.transform.parent.gameObject.SetActive(false);
        answer2Text.transform.parent.gameObject.SetActive(false);
        answer3Text.transform.parent.gameObject.SetActive(false);
        continueButton.SetActive(true);
    }

    public void SetQuestion()
    {
        
        if(QuestionsAsked.Count == loadedData.jsonObject.Count)
        {
            //reset the questions
            QuestionsAsked.Clear();
        }


        newNumber = GenerateNumber();
        // Questions newQuestion = new Questions();
        QuestionsAsked.Add(newNumber);
        mainText.text = loadedData.jsonObject[newNumber].Question;

        answer1Text.text = loadedData.jsonObject[newNumber].Answers[0];
        answer2Text.text = loadedData.jsonObject[newNumber].Answers[1];
        answer3Text.text = loadedData.jsonObject[newNumber].Answers[2];
        // newQuestion.Question = loadedData.jsonObject[i].Question;


    }

    int  GenerateNumber()
    {
        int randomNumber = 0;
        int counter = 0;
        bool numberFound = false;

        while (numberFound == false)
        {
            randomNumber = UnityEngine.Random.Range(0, loadedData.jsonObject.Count);
            numberFound = true;
            for (int i = 0; i < QuestionsAsked.Count; i++)
            {
                if (randomNumber == QuestionsAsked[i])
                {
                    numberFound = false;
                }
               
            }
            counter++;
            if (counter >= 100)
            {
                break;
            }
        }


        return randomNumber;
    }

    public void CheckAnswer(int ansNo)
    {
        DisableAnswers();
        //Add analytics in here
        if(ansNo.ToString() == loadedData.jsonObject[newNumber].CorrectAnswer)
        {
            mainText.text = loadedData.jsonObject[newNumber].CorrectExplanation;
        }
        else
        {
            mainText.text = loadedData.jsonObject[newNumber].IncorrectExplanation;
        }
    }
}

