using UnityEngine;

public class SkillObjectScript : MonoBehaviour
{
    public bool onClicked;
    [SerializeField] Transform skillImageTh;
    public GameObject skill;
    public GameObject skills;

    private void Update()
    {
        // 클릭하면 skill 이미지를 이미지 자리에 복사하고 skills 오브젝트 부모를 가진다.
        if (onClicked)
        {
            GameObject instantiateSkill = Instantiate(skill, skillImageTh.position, transform.rotation, skills.transform);
        }
    }
}
