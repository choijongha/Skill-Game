using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SkillUnit : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject skillInto;

    private RaycastHit2D hit;
    private GraphicRaycaster mRaycaster;
    private PointerEventData mPointerEventData;
    private EventSystem mEventSystem;

    GameObject skillInstantiation;
    public TextMeshProUGUI skillInfo;
    List<RaycastResult> results;
    private bool onClicked = false;

    public Movement playerScript;
    private void Awake()
    {
        mRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        mEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        playerScript = GameObject.Find("Player").GetComponent<Movement>();
    }
    private void Update()
    {
        MouseClickDown();
    }
    // 마우스 클릭 시 
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
                if (!onClicked)
                {
                    onClicked = true;
                    skillPanel.SetActive(true);
                    // 클릭된 오브젝트와 연결된 프리팹 스킬 이미지 오브젝트 연결.
                    skillInstantiation = skillObjectScript.InstantiateSkill();
                    skillInfo.text = skillInstantiation.GetComponent<SkillInspector>().skillinfo;
                    playerScript.skills.Add(skillInstantiation.GetComponent<SkillInspector>());
                } else if(onClicked && skillPanel.activeSelf)
                {
                    Destroy(skillInstantiation);
                    // 클릭된 오브젝트와 연결된 프리팹 스킬 이미지 오브젝트 연결.
                    skillInstantiation = skillObjectScript.InstantiateSkill();
                    skillInfo.text = skillInstantiation.GetComponent<SkillInspector>().skillinfo;
                } 
            }else if(hit.collider == null && results.Count <= 0)
            {
                onClicked = false;
                skillPanel.SetActive(false);
                skillInto.SetActive(false);
                Destroy(skillInstantiation);
            }
        }
    }
    // UI 클릭 함수
    private void UIClick()
    {
        //Set up the new Pointer Event
        mPointerEventData = new PointerEventData(mEventSystem);
        //Set the Pointer Event Position to that of the mouse position
        mPointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        mRaycaster.Raycast(mPointerEventData, results);

        // UI가 클릭 되었을 때만 작동.
        if(results.Count > 0)
        {
            Debug.Log(results[0].gameObject.name);
            // 클릭된 UI이름과 복사해 생긴 이미지 이름이 같다면 실행.
            if(results[0].gameObject.name == skillInstantiation.name)
            {
                if (skillInto.activeSelf == true)
                {
                    skillInto.SetActive(false);
                }
                else
                {
                    skillInto.SetActive(true);
                }
                    
            }
        }
    }

    
}
