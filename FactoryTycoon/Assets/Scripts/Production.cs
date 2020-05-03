using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public GameObject waste;
    public GameObject rawMat;
    public float wastePercent;
    GenerateGrid grid;
    float totalRawMat = 0;
    public GameObject smokeEffect;
    public AudioSource machineSound;


    List<GridTile> rawMats;
    List<GridTile> productionTiles;
    List<GridTile> DisposalTiles;
    List<GridTile> StorageTiles;
    List<GridTile> WasteTiles;
  
    private float time = 1;
    bool lerpIN = true;
    bool lerpYIN = true;
    float lerpX = 1;
 
    // Start is called before the first frame update
    void Start()
    {
        productionTiles = new List<GridTile>();
        DisposalTiles = new List<GridTile>();
        StorageTiles = new List<GridTile>();
        WasteTiles = new List<GridTile>();
        rawMats = new List<GridTile>();
        grid = FindObjectOfType<GenerateGrid>();
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //public void UseMaterials()
    //{

    //    UpdateStats();
    //    totalRawMat = 0;
    //    DisposalTiles.Clear();
    //    productionTiles.Clear();
    //    rawMats.Clear();



    //    for (int i = 0; i < grid.GetHeight(); i++)
    //    {
    //        for (int j = 0; j < grid.GetWidth(); j++)
    //        {
    //            GridTile tile = grid.GetGrid(i, j);
    //            if (tile.factorySection == "Storage")
    //            {

                    
    //                    if (CountProductionAndWaste())
    //                    {
    //                    if (tile.UpgradeSection == "RawMaterial")
    //                    {
    //                        rawMats.Add(tile);
    //                        // tile.ResetTile();

    //                        //  Debug.Log(tile.gameObject.transform.GetChild(0).name);
    //                        //   Destroy(tile.gameObject.transform.GetChild(0).gameObject);
    //                    }
    //                    }


    //            }


    //        }
    //    }

    //        if (productionTiles.Count >= 1 && DisposalTiles.Count >=1 )
    //        {
    //            UseProductionMachines();
    //            UseDisposal();
    //            CheckWasteStorage();
    //        }
    //    CountProductionAndWaste();
    //   if(productionTiles.Count ==0 && DisposalTiles.Count == 0)
    //    {
    //        if(FindObjectOfType<GameManager>().money < 700)
    //        {
    //            FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
    //        }
    //    }
    //   else if(productionTiles.Count == 0)
    //    {
    //        if (FindObjectOfType<GameManager>().money < 300)
    //        {
    //            FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
    //        }
    //    }
    //    else if (DisposalTiles.Count == 0)
    //    {
    //        if (FindObjectOfType<GameManager>().money < 400)
    //        {
    //            FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
    //        }
    //    }
    //    else if(totalRawMat == 0 &&  FindObjectOfType<GameManager>().money < 50)
    //    {
    //        FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
    //    }
    //    IncreaseTurnCount();


    //}


    //bool CountProductionAndWaste()
    //{
    //    productionTiles.Clear();
    //    DisposalTiles.Clear();
    //    for (int i = 0; i < grid.GetHeight(); i++)
    //    {
    //        for (int j = 0; j < grid.GetWidth(); j++)
    //        {
    //            GridTile tile = grid.GetGrid(i, j);

    //            if (tile.factorySection == "Factory")
    //            {
    //                if (tile.UpgradeSection == "Production")
    //                {
    //                    productionTiles.Add(tile);
    //                }
    //                else if (tile.UpgradeSection == "Disposal")
    //                {
    //                    DisposalTiles.Add(tile);
    //                }
    //            }
    //        }
    //    }

    //    if(productionTiles.Count >= 1 && DisposalTiles.Count >= 1)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    bool CountWaste(GridTile tile)
    {

        if (tile.UpgradeSection == "Waste")
        {
            return true;
        }
        return false;
    }

    bool CountRawMats(GridTile tile)
    {
        
        if(tile.UpgradeSection == "RawMaterial")
        {
            totalRawMat += tile.itemQuantity;
            return true;
        }
        return false;
    }

    //void UseProductionMachines()
    //{
    //    totalRawMat = rawMats.Count;
    //    int perMachine = Mathf.FloorToInt( totalRawMat / productionTiles.Count) ;
    //    int remainder = (int)totalRawMat % (int)productionTiles.Count;
    //    Debug.Log(perMachine + "total " + totalRawMat + " " + productionTiles.Count + " " + remainder);
    //    float totalSales = 0;

    //    GameManager gm = FindObjectOfType<GameManager>();

    //    for (int i = 0; i < productionTiles.Count; i++)
    //    {
    //        for(int j =0; j < perMachine; j++)
    //        {
    //            totalSales += rawMats[0].itemQuantity + gm.sellPrice;
                
    //            rawMats[0].ResetTile();
    //            Destroy(rawMats[0].gameObject.transform.GetChild(0).gameObject);
    //            rawMats.RemoveAt(0);
    //            // totalSales += productionTiles[i].itemQuantity * perMachine;
    //        }
    //    }
    //    if(remainder == 1)
    //    {
    //        totalSales += rawMats[0].itemQuantity + gm.sellPrice;
            
    //        rawMats[0].ResetTile();
    //        Destroy(rawMats[0].gameObject.transform.GetChild(0).gameObject);
    //        rawMats.RemoveAt(0);
    //        //totalSales += productionTiles[0].itemQuantity * remainder;
    //    }
    //    GameManager gameManager = FindObjectOfType<GameManager>();
    //    gameManager.UpdateMoney(totalSales);
    //}

    //void UseDisposal()
    //{
    //     int perMachine = Mathf.FloorToInt(totalRawMat / DisposalTiles.Count);
    //     int remainder = (int)totalRawMat % (int)DisposalTiles.Count;

    //    //add up percentages on disposal machines
       
    //    for (int i = 0; i < DisposalTiles.Count; i++)
    //    {
    //        //calculated the percentage chance of raw material
    //        for (int j = 0; j < perMachine; j++)
    //        {
    //            float percentageChance = (DisposalTiles[i].itemQuantity / 100);

    //            if(Random.value > percentageChance)
    //            {
    //                //waste
    //                GridTile gridTile = FindEmpty();
    //                gridTile.occupied = true;
    //                gridTile.itemQuantity = 1;
    //                gridTile.UpgradeSection = "Waste";
    //                Instantiate(waste, gridTile.transform);
    //            }
    //            else
    //            {
    //                //raw material
    //                GridTile gridTile = FindEmpty();
    //                gridTile.occupied = true;
    //                gridTile.itemQuantity = 1;
    //                //gridTile.tileUpgradeName = "RawMaterial";
    //                gridTile.UpgradeSection = "RawMaterial";
    //                // gridTile.transform.position;
    //                Instantiate(rawMat, gridTile.transform);

    //            }


    //        }

    //    }
    //    if (remainder == 1)
    //    {
    //        float percentageChance = (DisposalTiles[0].itemQuantity / 100);

    //        if (Random.value > percentageChance)
    //        {
    //            //waste
    //            GridTile gridTile = FindEmpty();
    //            gridTile.occupied = true;
    //            gridTile.itemQuantity = 1;
    //            gridTile.UpgradeSection = "Waste";
    //            Instantiate(waste, gridTile.transform);
    //        }
    //        else
    //        {
    //            //raw material
    //            GridTile gridTile = FindEmpty();
    //            gridTile.occupied = true;
    //            gridTile.itemQuantity = 1;
    //            //gridTile.tileUpgradeName = "RawMaterial";
    //            gridTile.UpgradeSection = "RawMaterial";
    //            // gridTile.transform.position;
    //            Instantiate(rawMat, gridTile.transform);

    //        }
    //    }

    //}

    GridTile FindEmpty()
    {

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (!tile.occupied && tile.factorySection == "Storage")
                {
                    return tile;
                }
            }
        }

        //no empty ones
        return null;
    }

  void UpdateStats()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateUI();
    }
    
        public void IncreaseTurnCount()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.IncreaseTurn();
    }

    void CheckWasteStorage()
    {
        StorageTiles.Clear();
        WasteTiles.Clear();

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);
                if (tile.factorySection == "Storage")
                {
                    StorageTiles.Add(tile);
                    if (CountWaste(tile))
                    {
                        WasteTiles.Add(tile);
                    }
                }
            }
        }
        float tiles = (float)WasteTiles.Count / (float)StorageTiles.Count;

        if ((tiles * 100)>= FindObjectOfType<Goal>().waste)
        {
            Debug.Log("Game over");
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver(GameManager.GameOverCode.Waste);
        }
    }
    public void FactoryProduction()
    {
        StartCoroutine(FactoryProcess());
    }

    IEnumerator FactoryProcess()
    {
        // find out how many raw mats
        GetRawMaterials();

        //find out production machines
        GetProductionMachines();
        GetDisposalMashines();


        if (productionTiles.Count >= 1 && DisposalTiles.Count >= 1)
        {
            //put raw mat through production

            for (int i = 0; i < rawMats.Count; i++)
            {
                //choose random production machine
                int machineNo = Random.Range(0, productionTiles.Count);
                int disposalMachine = Random.Range(0, DisposalTiles.Count);
                lerpIN = true;
                lerpYIN = true;

                //animation plays here
                machineSound.Play();
                StartCoroutine(AnimateMachineX(productionTiles[machineNo]));
                StartCoroutine(AnimateMachineX(DisposalTiles[disposalMachine]));
                StartCoroutine(AnimateMachineY(DisposalTiles[disposalMachine]));
                //wait till Y finished animating till moving on
                yield return StartCoroutine(AnimateMachineY(productionTiles[machineNo]));

                ResetSize(productionTiles[machineNo]);

                //calculate cost
                GameManager gameManager = FindObjectOfType<GameManager>();
                float totalmoney = DisposalTiles[disposalMachine].itemQuantity + gameManager.sellPrice;
                gameManager.UpdateMoney(totalmoney);
                UpdateStats();
                //poof effect
                GameObject poofEffect = Instantiate(smokeEffect);
                poofEffect.transform.position = new Vector3(rawMats[i].transform.position.x, rawMats[i].transform.position.y, -1);
                //waste appears
                float percentageChance = (productionTiles[machineNo].itemQuantity / 100);

                GridTile current = rawMats[i].GetComponent<GridTile>();
                Destroy(current.transform.GetChild(0).gameObject);

                if (Random.value > percentageChance)
                {
                    //waste
                    current.UpgradeSection = "Waste";
                    Instantiate(waste, current.transform);
                }
                else
                {
                    current.UpgradeSection = "RawMaterial";
                    Instantiate(rawMat, current.transform);

                }
            }
        }
        CheckTurn();
    }

    void GetRawMaterials()
    {
        rawMats.Clear();

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);

                if (tile.UpgradeSection == "RawMaterial")
                {
                    rawMats.Add(tile);
                }
            }
        }
    }

    void GetProductionMachines()
    {
        productionTiles.Clear();

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);

                if (tile.UpgradeSection == "Production")
                {
                    productionTiles.Add(tile);
                }
            }
        }
    }

    void GetDisposalMashines()
    {
        DisposalTiles.Clear();

        for (int i = 0; i < grid.GetHeight(); i++)
        {
            for (int j = 0; j < grid.GetWidth(); j++)
            {
                GridTile tile = grid.GetGrid(i, j);

                if (tile.UpgradeSection == "Disposal")
                {
                    DisposalTiles.Add(tile);
                }
            }
        }
    }

   IEnumerator AnimateMachineY(GridTile machine)
    {
        float timer = 0;
        float lerpTime = 0.2f;
        float lerp = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            lerp += Time.deltaTime;
            float t = 0;
            Transform machineTile = machine.gameObject.transform.GetChild(0);
            t = lerp / lerpTime;
            if (lerpIN)
            {

               
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                float lerpy = Mathf.Lerp(0.08f, 0.06f, t);
                machineTile.localScale = new Vector3(machineTile.localScale.x,lerpy, 1);
                if (machineTile.localScale.x <= 0.06f)
                {
                    lerpIN = false;
                    lerp = 0;
                }
            }
            else
            {
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                float lerpy = Mathf.Lerp(0.06f, 0.08f, t);
                machineTile.localScale = new Vector3(machineTile.localScale.x, lerpy, 1);
                if (machineTile.localScale.x >= 0.08f)
                {
                    lerpIN = true;
                    lerp = 0;
                }
            }
            yield return null;
        }
      
          yield return null;
    }
    IEnumerator AnimateMachineX(GridTile machine)
    {

        float timer = 0;
        float lerpTime = 0.1f;
        float lerp = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            lerp += Time.deltaTime;
            float t = 0;
            Transform machineTile = machine.gameObject.transform.GetChild(0);
            t = lerp / lerpTime;
            if (lerpIN)
            {


                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                lerpX = Mathf.Lerp(0.08f, 0.04f, t);
                machineTile.localScale = new Vector3(lerpX, machineTile.localScale.y, 1);
                if (machineTile.localScale.x <= 0.04f)
                {
                    lerpIN = false;
                    lerp = 0;
                }
            }
            else
            {
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                lerpX = Mathf.Lerp(0.04f, 0.08f, t);
                machineTile.localScale = new Vector3(lerpX, machineTile.localScale.y, 1);
                if (machineTile.localScale.x >= 0.08f)
                {
                    lerpIN = true;
                    lerp = 0;
                }
            }
            yield return null;
        }
        ResetSize(machine);
        yield return null;
    }
    void ResetSize(GridTile machine)
    {
        Transform machineTile = machine.gameObject.transform.GetChild(0);
        machineTile.localScale = new Vector3(0.08f, 0.08f, 1);
    }

    void CheckTurn()
    {
        CheckWasteStorage();

        if (productionTiles.Count == 0 && DisposalTiles.Count == 0)
        {
            if (FindObjectOfType<GameManager>().money < 700)
            {
                FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
            }
        }
        else if (productionTiles.Count == 0)
        {
            if (FindObjectOfType<GameManager>().money < 300)
            {
                FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
            }
        }
        else if (DisposalTiles.Count == 0)
        {
            if (FindObjectOfType<GameManager>().money < 400)
            {
                FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
            }
        }
        else if (totalRawMat == 0 && FindObjectOfType<GameManager>().money < 50)
        {
            FindObjectOfType<GameManager>().GameOver(GameManager.GameOverCode.NoMoney);
        }
        IncreaseTurnCount();


    }
}
