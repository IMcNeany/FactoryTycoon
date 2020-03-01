using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    [SerializeField]
    float bounceAmount;
    public Vector2 startPos;
    [SerializeField]
    Vector2 endPos;
    public bool bounce = true;
    bool forward = true;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(bounce)
        {
            if(forward)
            {
                if(Vector2.Distance(endPos, transform.localPosition) > 0.1f)
                {
                    //move
                    transform.localPosition = Vector2.Lerp(transform.localPosition, endPos, bounceAmount * Time.deltaTime);
                }
                else
                {
                    forward = false;
                }
            }
            if(!forward)
            {
                if (Vector2.Distance(transform.localPosition, startPos) > 0.1f)
                {
                    //move
                    transform.localPosition = Vector2.Lerp(transform.localPosition, startPos, bounceAmount * Time.deltaTime);
                }
                else
                {
                    forward = true;
                }
            }
        }
    }
}
