using System.Collections;
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
    protected float economical;
    protected float environmental;
    protected float social;
    protected float productionQuantity;
    public GameObject panel;
    public GameObject tileSprite;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameObject loadedPanel = Resources.Load("Upgrade Panel") as GameObject;

        //spawn the panel
        panel = Instantiate(loadedPanel);
        panel.SetActive(false);
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
