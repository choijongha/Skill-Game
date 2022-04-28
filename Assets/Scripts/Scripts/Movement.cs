using UnityEngine;
using System.Collections;
using AllUnits;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : Unit
{
    public Vector2 saveMaxPos { get; private set; }
    public Vector2 saveMinPos { get; private set; }
    public LevelDesign levelDesign;
    override protected void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
        AttackKey();
    }
    private void FixedUpdate()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        Vector2 playerPosition = target.transform.position - transform.position;
        rb.transform.position += (Vector3)playerPosition.normalized * speed * Time.deltaTime;
        float moveX = playerPosition.x;
        float moveY = playerPosition.y;
        unitAnimator.SetFloat("MoveX", moveX);
        unitAnimator.SetFloat("MoveY", moveY);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            unitAnimator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            unitAnimator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            unitAnimator.SetBool("IsMoving", true);
        }
    }
    private void AttackKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttackDelay)
        {
            unitAnimator.SetTrigger("Attacking");
            isAttackDelay = true;
            unitAnimator.SetBool("IsIdle",false);
            unitAnimator.SetBool("IsMoving",false);
            unitAnimator.SetFloat("AttackSpeed", attackSpeed);
        }
        else if (Input.GetKey(KeyCode.Space) && !isAttackDelay)
        {
            unitAnimator.SetTrigger("Attacking");
            isAttackDelay = true;
            unitAnimator.SetBool("IsIdle", false);
            unitAnimator.SetBool("IsMoving", false);
        }
    }
}

