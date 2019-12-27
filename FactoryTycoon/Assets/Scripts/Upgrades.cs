using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    protected string title;
    protected string description;
    protected Sprite image;
    protected float cost;
    protected GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = Resources.Load("Panel") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
