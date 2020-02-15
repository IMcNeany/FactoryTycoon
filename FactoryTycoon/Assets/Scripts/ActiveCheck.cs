using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCheck : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.tutorialActive)
        {
            this.gameObject.SetActive(false);
            
        }
        Destroy(this.GetComponent<ActiveCheck>(), 0);
    }
}
