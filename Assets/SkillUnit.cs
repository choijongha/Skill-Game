using UnityEngine;
public class SkillUnit : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    private Camera mainCamera;
    private RaycastHit2D hit;
    private bool onClicked;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        MouseClickDown();
    }
    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (!onClicked)
            {
                onClicked = true;
                skillPanel.SetActive(true);
            }
            else
            {
                onClicked = false;
                skillPanel.SetActive(false);
            }
            
        }
    }
    
}
