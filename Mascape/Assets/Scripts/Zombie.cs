using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Zombie : Enemy
{
    [Tooltip("Distance at which the zombie can attack")]
    public float AttackRange = 1.0f;
    [Tooltip("Speed at which the zombie can attack in seconds")]
    public float AttackSpeed = 1.0f;

    private Animator animator;
    private float attackTimeRemaining;
    private bool isAttacking = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            AttackPlayer(Target);
        }
        UpdateAttackStatus();
    }
    private void AttackPlayer(Friendly player)
    {
        player.Die();
        Moveable = false;
        animator.SetTrigger("Attack");
        attackTimeRemaining = AttackSpeed;
    }
    private void UpdateAttackStatus()
    {
        if (attackTimeRemaining > 0)
        {
            attackTimeRemaining -= Time.deltaTime;
            if (attackTimeRemaining <= 0)
            {
                isAttacking = false;
            }
            if (!isAttacking)
            {
                attackTimeRemaining = 0;
                animator.SetTrigger("Stop Attacking");
                Moveable = true;
            }
        }
    }
}
