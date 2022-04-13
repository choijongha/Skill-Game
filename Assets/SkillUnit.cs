using UnityEngine;
public class SkillUnit : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    private SkillObjectScript skillObject;
    private Camera mainCamera;
    private RaycastHit2D hit;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        skillObject = GameObject.FindGameObjectWithTag("SkillObject").GetComponent<SkillObjectScript>();
    }
    private void Update()
    {
        MouseClickDown();
    }
    private void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            Debug.Log(hit.collider);
            if(hit.collider.tag == "SkillObject")
            {
                if (!skillObject.onClicked)
                {
                    skillObject.onClicked = true;
                    skillPanel.SetActive(true);
                }
                else
                {
                    skillObject.onClicked = false;
                    skillPanel.SetActive(false);
                }
            }  
        }
    }
    
}
