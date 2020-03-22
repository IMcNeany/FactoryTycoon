using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGoogleDrive;
using System.IO;

public class UploadFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            AttemptUpload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptUpload()
    {
        if (!CheckInternet())
        {
            return;
        }

            StartCoroutine (CheckFilesExists());


        //  Debug.Log(list.ResponseData);

        // if(list.ResponseData == null)
        //{
        //    GoogleDriveFiles.Create(file).Send();
        //
        // }
        // GoogleDriveFiles.Create(file).Send();
        // GoogleDriveFiles.Create()
     
    }
    IEnumerator CheckFilesExists()
    {
     //   GoogleDriveRequest googleDriveRequest;
       // googleDriveRequest.Uri
        UnityGoogleDrive.GoogleDriveFiles.ListRequest list = GoogleDriveFiles.List();
        list.Q = "name = 'Saved_data.csv'";

        list.Send();
        list.OnDone += GetResults;
        yield return null;

        /*        while (list.Progress < 1.0f && list.IsRunning)
                {
                    Debug.Log(list.Progress);
                }

                while (!list.IsDone && !list.IsError)
                {
                    Debug.Log(list.Progress);
                }
                if (list.IsError)
                {

                }
                else if(list.IsDone)
                {
                   Debug.Log(list.ResponseData);
                }
                UnityGoogleDrive.Data.ResourceData data;
                data = list.GetResponseData<UnityGoogleDrive.Data.ResourceData>();*/

    }

    void GetResults(UnityGoogleDrive.Data.FileList fileList)
    {
        byte[] content = File.ReadAllBytes(getPath());
        UnityGoogleDrive.Data.File file = new UnityGoogleDrive.Data.File() { Name = Path.GetFileName(getPath()), Content = content };

        if (fileList.Files.Count >= 1)
        {
            for(int i =0; i < fileList.Files.Count; i ++)
            {
                if (fileList.Files[i].Name == "Saved_data.csv") 
                {

                    GoogleDriveFiles.Update(fileList.Files[i].Id, file).Send();
                }
            }
        }
        else
        {
            GoogleDriveFiles.Create(file).Send();
        }
        Debug.Log("done");
    }
    bool CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
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
