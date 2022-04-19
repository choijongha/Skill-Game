using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUnit : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject skillInto;
    private SkillObjectScript skillObject;
    private Camera mainCamera;
    private RaycastHit2D hit;

    private GraphicRaycaster mRaycaster;
    private PointerEventData mPointerEventData;
    private EventSystem mEventSystem;
    GameObject skillInstantiation;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        skillObject = GameObject.FindGameObjectWithTag("SkillObject").GetComponent<SkillObjectScript>();
        
        mRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        mEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

    }
    private void Update()
    {
        MouseClickDown();
    }
    private void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIClick();
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                SkillObjectScript skillObjectScript = GameObject.Find(hit.collider.name).GetComponent<SkillObjectScript>();
                
                if (!skillObjectScript.onClicked)
                {
                    skillObjectScript.onClicked = true;
                    skillPanel.SetActive(true);
                    skillInstantiation = skillObjectScript.InstantiateSkill();
                }
                else
                {
                    skillObjectScript.onClicked = false;
                    skillPanel.SetActive(false);
                    Destroy(skillInstantiation);
                }
            }
        }
    }
    private void UIClick()
    {
        //Set up the new Pointer Event
        mPointerEventData = new PointerEventData(mEventSystem);
        //Set the Pointer Event Position to that of the mouse position
        mPointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        mRaycaster.Raycast(mPointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
    }

    
}
