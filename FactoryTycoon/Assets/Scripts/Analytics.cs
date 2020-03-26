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
     int ID = 0;
    string difficultyLevel;
    public bool statsSaved = false;
    bool CollectionAgreed = false;

    private List<string[]> saveData = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        CollectionAgreed = FindObjectOfType<UploadFile>().analticsAgreed;
        if (CollectionAgreed)
        {
            if (!File.Exists(getPath()))
            {
                FirstCSVSetup();
            }
            else
            {
                GetLastRowUsed();
            }
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
        ID = 1;
    }

    void GetLastRowUsed()
    {
        string fileData = System.IO.File.ReadAllText(getPath());
       

        string[] lines = fileData.Split("\n"[0]);




        string[] lineData = (lines[(lines.Length-2)].Trim()).Split(","[0]);
       

        if(lineData[0] == "ID")
        {
            ID = 1;
        }
        else
        {
           
            int.TryParse(lineData[0].ToString(),out int tmpID);
        
            ID = tmpID;
            ID++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameComplete != true)
        {
            timer += Time.deltaTime;
        }
    }


    public void SaveStats()
    {
        if (CollectionAgreed)
        {
            saveData.Clear();
            GetStats();


            string[] data = new string[11];
            data[0] = ID.ToString();
            data[1] = timer.ToString();
            data[2] = gameComplete.ToString();
            data[3] = Win.ToString();
            data[4] = lose.ToString();
            data[5] = difficultyLevel;
            data[6] = social.ToString();
            data[7] = environmental.ToString();
            data[8] = economical.ToString();
            data[9] = money.ToString();
            data[10] = moreinfoClicked.ToString();
            saveData.Add(data);

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

            StreamWriter outStream = System.IO.File.AppendText(filePath);

            outStream.Write(sb);
            outStream.Close();

            FindObjectOfType<UploadFile>().AttemptUpload();
            statsSaved = true;
        }
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
        moreinfoClicked = gameManager.moreInfoEngagement;
        difficultyLevel = gameManager.difficultyLevel;

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
