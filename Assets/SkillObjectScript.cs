using UnityEngine;

public class SkillObjectScript : MonoBehaviour
{
    public bool onClicked;
    [SerializeField] Transform skillImageTh;
    public GameObject skill;
    public GameObject skills;

    private void Update()
    {
        // Ŭ���ϸ� skill �̹����� �̹��� �ڸ��� �����ϰ� skills ������Ʈ �θ� ������.
        if (onClicked)
        {
            GameObject instantiateSkill = Instantiate(skill, skillImageTh.position, transform.rotation, skills.transform);
        }
    }
}
