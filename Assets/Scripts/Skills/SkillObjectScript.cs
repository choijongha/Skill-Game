using UnityEngine;


public class SkillObjectScript : MonoBehaviour
{
    [SerializeField] Transform skillImageTh;
    public GameObject skill;
    public GameObject skills;
    
    public GameObject InstantiateSkill()
    {
        GameObject instantiateSkill = Instantiate(skill, skillImageTh.position, transform.rotation, skills.transform);
        return instantiateSkill;
    }
    
}
