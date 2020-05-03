using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePopUp : MonoBehaviour
{
    public GameObject popUp;

    bool opened;

   
    public void onButtonPressed()
    {
        opened = popUp.activeSelf;

        if (opened)
        {
            popUp.SetActive(false);
            opened = false;
        }
        else
        {
            popUp.SetActive(true);
            opened = true;
        }
    }

}
