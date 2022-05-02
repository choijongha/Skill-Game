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
    // ���콺 Ŭ�� �� 
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
                    // Ŭ���� ������Ʈ�� ����� ������ ��ų �̹��� ������Ʈ ����.
                    skillInstantiation = skillObjectScript.InstantiateSkill();
                    skillInfo.text = skillInstantiation.GetComponent<SkillInspector>().skillinfo;
                    playerScript.skills.Add(skillInstantiation.GetComponent<SkillInspector>());
                } else if(onClicked && skillPanel.activeSelf)
                {
                    Destroy(skillInstantiation);
                    // Ŭ���� ������Ʈ�� ����� ������ ��ų �̹��� ������Ʈ ����.
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
    // UI Ŭ�� �Լ�
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

        // UI�� Ŭ�� �Ǿ��� ���� �۵�.
        if(results.Count > 0)
        {
            Debug.Log(results[0].gameObject.name);
            // Ŭ���� UI�̸��� ������ ���� �̹��� �̸��� ���ٸ� ����.
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
