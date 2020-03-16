using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    GameManager gameManager;
    bool gameComplete = false;
    bool Win = false;
    bool lose = false;
    float social = -10;
    float environmental = -10;
    float economical = -10;
    float money = -10;
    int moreinfoClicked = 0;
    float timer = 0;

    private List<string[]> saveData = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
      
        if (!File.Exists(getPath()))
        {
            FirstCSVSetup();
        }
      
    }

    void FirstCSVSetup()
    {
        string[] headerRow = new string[11];
        headerRow[0] = "ID";
        headerRow[1] = "Time Spent";
        headerRow[2] = "Game Complete";
        headerRow[3] = "Win";
        headerRow[4] = "Lose";
        headerRow[5] = "Difficulty Level";
        headerRow[6] = "Social Stat";
        headerRow[7] = "Environmental Stat";
        headerRow[8] = "Economical Stat";
        headerRow[9] = "Money";
        headerRow[10] = "More Info Button Clicked";
        saveData.Add(headerRow);

        CreateDataFile();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameComplete != true)
        {
            timer += Time.deltaTime;
        }
    }
    //id
    //time
    //win / lose
    //more info button clicked
    //social stat
    //eco stat
    //econmical stat
    //money stat

    public void SaveStats()
    {
        GetStats();
        CreateDataFile();
    }

    public void MoreInfoButtonClicked()
    {
        moreinfoClicked++;
    }

    void GetStats()
    {
        social = gameManager.social;
        environmental = gameManager.environment;
        economical = gameManager.economic;
        money = gameManager.money;
        gameComplete = gameManager.completedLevel;
        lose = gameManager.lose;
        Win = gameManager.win;

    }

    void CreateDataFile()
    {

        string[][] output = new string[saveData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = saveData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}
