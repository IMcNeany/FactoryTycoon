using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{
    public ParticleSystem smog;
    public List<GameObject> houses;
     List<GameObject> activehouses;
     List<GameObject> inactivehouses;
    GameManager gameManager;
    int numActiveHouses;
    float previousSocialScore = 0;
   // float previousEnvironmentalScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        activehouses =  new List<GameObject>();
        inactivehouses = new List<GameObject>();

        for (int i = 0; i < houses.Count; i++)
        {
            houses[i].gameObject.SetActive(false);
            inactivehouses.Add ( houses[i].gameObject);
        }
        previousSocialScore = gameManager.social;
       SetParticleSystem((int)gameManager.environment);
        SetActiveHouses((int)(houses.Count/2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBackground()
    {
        float newSocial = gameManager.social;
        float newEnvironmental = gameManager.environment;

        float newActiveHouses = newSocial -previousSocialScore;
        previousSocialScore = newSocial;
        if(newActiveHouses > 0)
        {
            SetActiveHouses((int)newActiveHouses);
        }
        else if(newActiveHouses ==0)
        {

        }
        else
        {
            SetInactiveHouses(Mathf.Abs((int)newActiveHouses));
        }

        SetParticleSystem((int)newEnvironmental);
    }

    void SetActiveHouses(int totalactive)
    {
       // int remainingActive = totalactive - activehouses.Count;
        if (totalactive >= 0)
        {
            if (inactivehouses.Count > inactivehouses.Count- totalactive)
            {
                for (int i = 0; i < totalactive; i++)
                {
                    int randomHouse = Random.Range(0, inactivehouses.Count);
                    inactivehouses[randomHouse].gameObject.SetActive(true);
                    activehouses.Add(inactivehouses[randomHouse].gameObject);
                    inactivehouses.RemoveAt(randomHouse);
                }
            }
            else
            {
                for (int i = 0; i < inactivehouses.Count; i++)
                {
                    inactivehouses[i].gameObject.SetActive(true);
                    activehouses.Add(inactivehouses[i].gameObject);
                }
                inactivehouses.Clear();
            }
        }
        else
        {
            SetInactiveHouses(Mathf.Abs(totalactive));
        }

    }

    void SetInactiveHouses(int inactiveNo)
    {
        if (inactiveNo < activehouses.Count)
        {
            for (int i = 0; i < inactiveNo; i++)
            {
                int randomHouse = Random.Range(0, activehouses.Count);
                activehouses[randomHouse].gameObject.SetActive(false);
                inactivehouses.Add(activehouses[randomHouse].gameObject);
                activehouses.RemoveAt(randomHouse);
            }
        }
        //make all inactive
        else
        {
            for (int i = 0; i < activehouses.Count; i++)
            {
                activehouses[i].gameObject.SetActive(false);
                inactivehouses.Add(activehouses[i].gameObject);
            }
            activehouses.Clear();
        }
    }

    void SetParticleSystem(int newEnvironmental)
    {
        ParticleSystem.MainModule module = smog.main;

        float alpha = 1.0f;
        if (newEnvironmental <= -10)
        {
            module.startColor =  new Color(0,0,0, module.startColor.color.a);
        }
        else
        {
            module.startColor = new Color(1, 1, 1, module.startColor.color.a);
        }

        if(newEnvironmental == 0)
        {
            alpha = 0.2f;
        }
        else if (newEnvironmental >= 1 && newEnvironmental < 10)
        {
            alpha = 0.1f;
        }
        else if(newEnvironmental > 10)
        {
            alpha = 0.0f;
        }
        else if(newEnvironmental >= -2 && newEnvironmental < 0)
        {
            alpha = 0.3f;
        }
        else if (newEnvironmental >= -5 && newEnvironmental < -2)
        {
            alpha = 0.35f;
        }
        else if (newEnvironmental >= -8 && newEnvironmental < -5)
        {
            alpha = 0.35f;
        }
        else if (newEnvironmental < -8)
        {
            alpha = 0.5f;
        }

        module.startColor = new Color(module.startColor.color.r, module.startColor.color.g, module.startColor.color.b, alpha);
    }
}
