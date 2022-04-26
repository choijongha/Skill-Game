using UnityEngine;
using TMPro;
public class LevelDesign : MonoBehaviour
{
    [SerializeField] int maxLevel;
    [SerializeField] int currentLevel;
    [SerializeField] int[] expToLevelUp;
    [SerializeField] int baseExp;

    private Movement playerScript;
    private EnemyController monsterScript;
    private int countLevelup = 0;

    public int currentExp { get; set; }
    [SerializeField] TextMeshProUGUI text;

    public int levelUpMove = 0;
    public int levelUpMaxHealth = 0;
    public int levelUpDamageUp = 0;
    public int levelUpAttackSpeed = 0;

    [HideInInspector] public float initialMove = 0;
    [HideInInspector] public int initialMaxHealth = 0;
    [HideInInspector] public int initialDamageUp = 0;
    [HideInInspector] public float initialAttackSpeed = 0;
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }
    private void Start()
    {
        expForLevelUp();
        InitialStat();
    }
    private void Update()
    {
        levelUpSystem();
    }
    // 레벨업에 필요한 경험치 설정
    private void expForLevelUp()
    {
        text.text = "Level : " + currentLevel;
        expToLevelUp = new int[maxLevel];
        expToLevelUp[0] = baseExp;
        for (int i = 0; i < expToLevelUp.Length; i++)
        {
            if (i > 0)
            {
                expToLevelUp[i] = Mathf.FloorToInt(expToLevelUp[i - 1] * 1.05f);
            }
        }
    }
    // 레벨업하는 시스템
    private void levelUpSystem()
    {
        if (expToLevelUp[currentLevel - 1] <= currentExp)
        {
            int initialExp = currentExp - expToLevelUp[currentLevel - 1];
            currentLevel++;
            Debug.Log("Level Up");
            text.text = "Level : " + currentLevel;
            currentExp = initialExp;
            levelUpToStatUp();
        }
    }
    // 초기 능력치
    private void InitialStat()
    {
        initialMove = playerScript.speed;
        initialMaxHealth = playerScript.maxHealth;
        initialDamageUp = playerScript.damage;
        initialAttackSpeed = playerScript.attackSpeed;
    }
    // 레벨업 시 능력치 추가
    private void levelUpToStatUp()
    {
        levelUpMaxHealth += 10;
        playerScript.maxHealth = initialMaxHealth + levelUpMaxHealth;
        playerScript.currentHealth += 10;

        levelUpDamageUp += 2;
        playerScript.damage = initialDamageUp + levelUpDamageUp;
        
        if(currentLevel == 10)
        {
            levelUpAttackSpeed += 1;
            playerScript.attackSpeed = initialAttackSpeed + levelUpAttackSpeed;
            levelUpMove += 1;
            playerScript.speed = initialMove + levelUpMove;
        }
        else if(currentLevel > 10)
        {
            countLevelup++;
            if(countLevelup == 10)
            {
                countLevelup = 0;
                levelUpAttackSpeed += 1;
                playerScript.attackSpeed = initialAttackSpeed + levelUpAttackSpeed;

                levelUpMove += 1;
                playerScript.speed = initialMove + levelUpMove;
            }
        } 
    }
}