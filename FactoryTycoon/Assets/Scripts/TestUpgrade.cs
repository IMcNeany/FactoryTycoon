using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgrade : Upgrade
{
    [SerializeField]
    string itemTitle;
    [SerializeField]
    string itemDesc;
    [SerializeField]
    string altDesc;
    [SerializeField]
    float itemCost;
    [SerializeField]
    float itemSocial;
    [SerializeField]
    float itemEnvironmental;
    [SerializeField]
    float itemEconomic;
    [SerializeField]
    GameObject sprite;
    [SerializeField]
    string SectionName;
    [SerializeField]
    float productionQuant;
    // Start is called before the first frame update
    protected override void Start()
    {

        Upgrade parentUpgrade = this.gameObject.GetComponent<Upgrade>();
        parentUpgrade.SetDesc(itemTitle, itemDesc, itemCost, itemSocial, itemEnvironmental, itemEconomic, sprite, SectionName, productionQuant, altDesc);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
