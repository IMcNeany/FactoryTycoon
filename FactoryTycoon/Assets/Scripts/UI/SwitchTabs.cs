using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTabs : MonoBehaviour
{
    public List<GameObject> tabs;
    // Start is called before the first frame update
    void Start()
    {
        OpenTab(0);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTab(int tabNo)
    {
        for(int i = 0; i < tabs.Count; i++)
        {
            if(tabNo == i)
            {
                tabs[i].SetActive(true);
             
            }
            else
            {
                tabs[i].SetActive(false);
               
            }
        }
    }
}
