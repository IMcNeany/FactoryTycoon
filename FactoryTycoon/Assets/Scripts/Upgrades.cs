﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    protected string title;
    protected string description;
    protected string altDescription;
    protected string itemUpgradeSection;
    protected Sprite image;
    protected float cost;
    protected float environmental;
    protected float social;
    protected float productionQuantity;
    protected int upgradeNumber = 1;
    public GameObject panel;
    public GameObject tileSprite;
    // Start is called before the first frame update
    protected virtual void Start()
    {
       // GameObject loadedPanel = Resources.Load("Upgrade Panel") as GameObject;

        //spawn the panel
      //  Instantiate(panel);
    //    panel.SetActive(false);
    }

   protected GameObject GetPanelRef()
    {
        return panel;
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }

   protected virtual void OnButtonClicked()
    {

    }
}
