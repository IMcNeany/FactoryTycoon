using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleUpgrade : Upgrades
{
    // Start is called before the first frame update
    void Start()
    {
        title = "item Title";
        description = "content description of upgrade/machine" +
            "Social: +2" +
            "Ecomical: +3" +
            "Enviromental: -2";
        cost = 100000;

    }

    void UpdatePanel()
    {
        //turn to list
        TextMeshProUGUI titleTextBox =  panel.transform.GetChild(0).transform.GetComponentInChildren<TextMeshProUGUI>();
        if(titleTextBox.gameObject.name == "Title")
        {
            titleTextBox.SetText(title);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
