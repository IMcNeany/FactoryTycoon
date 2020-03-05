using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIRayCaster : MonoBehaviour
{
    public GameManager gameManager;
    private PointerEventData m_PointerEventData;
    private GraphicRaycaster m_Raycaster;
    private EventSystem m_EventSystem;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        m_Raycaster = this.GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.checkUIRayCast == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
               
                m_PointerEventData.position = Input.mousePosition;
                //  m_PointerEventData.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    Debug.Log(result.gameObject.name);
                    CheckResult(result);
                    
                }
            }
        }
    }

    void CheckResult(RaycastResult result)
    {
        if(result.gameObject.name == "Purchase")
        {
           Upgrade upgrade = gameManager.activeUpgrade;
            upgrade.PlaceUpgrade();
       
        }
    }
}
