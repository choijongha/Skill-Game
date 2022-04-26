using UnityEngine;
using System.Collections;
using AllUnits;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : Unit
{
    private AreaTransitionScript boundaryScript;
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
        // Rigidbody2D.velocity
        /*playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ;
        unitAnimator.SetFloat("MoveX", playerRb.velocity.x);
        unitAnimator.SetFloat("MoveY", playerRb.velocity.y);
        */

        // Transform.position
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 moveVector = new Vector2(moveX, moveY);
        rb.transform.position += moveVector.normalized * speed * Time.deltaTime;
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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Boundary")
        {
            boundaryScript = collision.GetComponent<AreaTransitionScript>();
            saveMaxPos = boundaryScript.newMaxCameraBoundary;
            saveMinPos = boundaryScript.newMinCameraBoundary;
        }
    }
}

