using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    Vector3 startRotation;
    bool startRotate = true;
    public float axisZSpeed = 5.0f;
    public float axisXSpeed = 0.0f;
    public float axisYSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(startRotate)
        {
            Rotate();
        }
    }

    void Rotate()
    {
        transform.Rotate( new Vector3(axisXSpeed * Time.deltaTime, axisYSpeed * Time.deltaTime, axisZSpeed * Time.deltaTime));
    }
}
