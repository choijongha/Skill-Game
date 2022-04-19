using UnityEngine;

public class SkillObjectScript : MonoBehaviour
{
    public bool onClicked;
    [SerializeField] Transform skillImageTh;
    public GameObject skill;
    public GameObject skills;
    public GameObject InstantiateSkill()
    {
        GameObject instantiateSkill = Instantiate(skill, skillImageTh.position, transform.rotation, skills.transform);
        return instantiateSkill;
    }
    
}
