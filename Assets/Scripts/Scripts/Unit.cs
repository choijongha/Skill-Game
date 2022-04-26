using System;
using TMPro;
using UnityEngine;

namespace AllUnits
{
    public class Unit : MonoBehaviour
    {
        // 플레이어와 적 유닛이 공통으로 사용할 스텟
        [SerializeField] internal float speed = 3f;
        [SerializeField] internal int maxHealth = 50;
        [SerializeField] internal int currentHealth;
        [SerializeField] internal int damage = 5;
        [SerializeField] internal float damageDelay = 2f;
        [SerializeField] internal float attackSpeed = 1f;
        [SerializeField] internal float attackDelay = 1f;
        // 유닛 상태
        protected bool isDamage = false;
        protected bool isAttackDelay = false;       
        public bool isDead = false;
        protected Animator unitAnimator;
        // 데미지 입은 상태에 쓰이는 변수
        [SerializeField] protected float damageFlashInterval = 0f;
        [SerializeField] protected float damageBound = 0f;
        private float initialDamageDelay;
        private float initialDamageFlashInterval;
        internal float initialAttackDelay;
        // 참조 변수
        protected SpriteRenderer spriteRenderer;
        protected Rigidbody2D rb;
        // 기타 변수
        internal float defaultAttackDelay;
        public int exp; 
        virtual protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            unitAnimator = GetComponent<Animator>();
        }
        virtual protected void Start()
        {
            currentHealth = maxHealth;
            initialDamageDelay = damageDelay;
            initialDamageFlashInterval = damageFlashInterval;
            initialAttackDelay = attackDelay;
            defaultAttackDelay = initialAttackDelay;
        }
        virtual protected void Update()
        {
            DamageDelay();
            AttackDelay();
            AttackTotalSpeed();
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        virtual protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "HitBox" && !isDead)
            {
                Vector3 offset = new Vector2(0.5f, 0.7f);
                bool isCritical = UnityEngine.Random.Range(0, 100) < 30;
                // 적이 플레이어 공격
                if (collision.GetComponentInParent<EnemyController>() && !isDamage)
                {
                    // 공격 당할 때
                    int hitdamageEnemy = collision.GetComponentInParent<EnemyController>().damage;
                    float damageRandom = UnityEngine.Random.Range(hitdamageEnemy, hitdamageEnemy * 1.5f);
                    int criticalDamage = (int)damageRandom * 2;
                    isDamage = true;
                    
                    if (!isCritical)
                    {
                        currentHealth -= (int)damageRandom;
                        DamagePopup.Create(transform.position + offset, (int)damageRandom, isCritical);
                    }
                    else
                    {
                        currentHealth -= criticalDamage;
                        DamagePopup.Create(transform.position + offset, criticalDamage, isCritical);
                    }  
                    // 죽는다면
                    if (currentHealth <= 0)
                    {
                        isDead = true;
                        gameObject.SetActive(false);
                    }
                // 플레이어가 적 공격    
                }else if (collision.GetComponentInParent<Movement>())
                {
                    // 적이 플레이어에게 공격 당할 때
                    Movement hitdamagePlayer = collision.GetComponentInParent<Movement>();
                    float damageRandom = UnityEngine.Random.Range(hitdamagePlayer.damage, hitdamagePlayer.damage * 1.5f);
                    int criticalDamage = (int)damageRandom * 2;
                    if (!isCritical)
                    {
                        currentHealth -= (int)hitdamagePlayer.damage;
                        DamagePopup.Create(transform.position + offset, (int)damageRandom, isCritical);
                    }
                    else
                    {
                        currentHealth -= criticalDamage;
                        DamagePopup.Create(transform.position + offset, criticalDamage, isCritical);
                    }   
                    unitAnimator.SetTrigger("Hurt");                    
                    // 적이 죽는다면
                    if (currentHealth <= 0)
                    {
                        isDead = true;
                        unitAnimator.SetBool("Died", true);
                        hitdamagePlayer.levelDesign.currentExp += exp;
                        Destroy(gameObject, 1f);
                    }
                }
            }
        }
        // 데미지 입고 무적 시간
        protected void DamageDelay()
        {
            if (isDamage && damageDelay > 0)
            {
                damageDelay -= Time.deltaTime;
                DamageDelayAction();
                if (damageDelay <= 0)
                {
                    isDamage = false;
                    damageDelay = initialDamageDelay;
                    damageFlashInterval = initialDamageFlashInterval;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                }
            }
        }
        // 무적 시간 표현
        protected void DamageDelayAction()
        {
            var flashView = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            var flashHide = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
            if (damageDelay < initialDamageDelay - damageFlashInterval)
            {
                damageFlashInterval += initialDamageFlashInterval;
                
                if (spriteRenderer.color == flashView)
                {
                    spriteRenderer.color = flashHide; 
                } else if (spriteRenderer.color == flashHide)
                {
                    spriteRenderer.color = flashView;
                }
            }
            else if (damageDelay > initialDamageDelay * 0.99f)
            {
                spriteRenderer.color = flashHide;
            }
        }
        // 연속 공격 시 공격 사이 쿨타임
        protected void AttackDelay()
        {
            if (isAttackDelay)
            {
                attackDelay -= Time.deltaTime;
                unitAnimator.SetBool("IsIdle", true);
                if (attackDelay <= 0)
                {
                    isAttackDelay = false;
                    attackDelay = initialAttackDelay;
                }
            }
        }
        public void AttackTotalSpeed()
        {
            initialAttackDelay = defaultAttackDelay / attackSpeed;
        }
    }
    
}