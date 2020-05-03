using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    Vector3 startScale;
    public float EndScale = 3.4f;
    public float timer = 5;
    float time = 0;
    bool lerpOut = true;
    float lerp;
    public float lerpTime = 1;
    public int targetLoopNo = 2;
    public bool play = true;
    int loopNo = 0;
    float resetTimer = 1;
    public bool autoTurnoff = false;

    // Start is called before the first frame update
    void Start()
    {
        startScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (play == true)
        {
            time += Time.deltaTime;

            if (time >= timer)
            {
                Pulsing();
            }
        }
        else if(transform.localScale != startScale)
        {
            StartCoroutine(ScaleBack());
        }
    
      
    }

    void Pulsing()
    {
        if (targetLoopNo != loopNo)
        {
            lerp += Time.deltaTime;
            float t = 0;
            t = lerp / lerpTime;
            if (lerpOut)
            {
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                float lerpy = Mathf.Lerp(startScale.x, EndScale, t);
                transform.localScale = new Vector3(lerpy, lerpy, 1);
                if (transform.localScale.x >= EndScale)
                {
                    lerpOut = false;
                    lerp = 0;
                }
            }
            else
            {
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                float lerpy = Mathf.Lerp(EndScale, startScale.x, t);
                transform.localScale = new Vector3(lerpy, lerpy, 1);
                if (transform.localScale.x <= (startScale.x + 0.02))
                {
                    lerpOut = true;
                    lerp = 0;
                    loopNo++;
                }
            }
        }
        else
        {
            loopNo = 0;
            time = 0;
          
            if(autoTurnoff)
            {
                play = false;
                StartCoroutine(ScaleBack());
            }
        }
    }

    IEnumerator ScaleBack()
    {
        float resetTime = 0;
        while (resetTime < resetTimer)
        {
            lerp += Time.deltaTime;
            float t = 0;
            t = lerp / 1.0f;

            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            float lerpy = Mathf.Lerp(transform.localScale.x, startScale.x, t);
            transform.localScale = new Vector3(lerpy, lerpy, 1);
            if (transform.localScale.x <= (startScale.x + 0.02))
            {
                transform.localScale = startScale;
                resetTime = 5;
               
            }
                yield return null;
        }
        yield return null;
    }
}
