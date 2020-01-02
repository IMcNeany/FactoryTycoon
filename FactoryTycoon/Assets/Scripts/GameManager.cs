using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int environment = 0;
    public int economic = 0;
    public int social = 0;
    public float money = 1000;

    public GameObject spriteToPlace = null;

    public GameObject uiUpgrades;
    public bool checkRayCast = true;
    public bool checkUIRayCast = true;

    public ExampleUpgrade activeUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseUpgradeUI()
    {
        uiUpgrades.SetActive(false);
    }
}
