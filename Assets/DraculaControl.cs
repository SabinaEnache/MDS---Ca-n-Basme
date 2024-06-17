using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaControl : MonoBehaviour, IEnemyController
{
    public float speed;
    public float range;
    public int firstTimeDed = 0;
    public CharacterController controller;
    public Transform player;

    public float RangeDeAttack = 1;

    public float ImpactTime = 0.70f;
    public int Health;
    public int damage;
    private Animator animator;
    private float attackCooldown = 2.0f; // Cooldown de atac de 2 secunde
    private float lastAttackTime = -2.0f; // Timpul ultimei atacuri
    private bool impacted = true;
    private Fighter4 opponent;


    void Start()
    {
        Health = 100;
        animator = GetComponent<Animator>();
        opponent = player.GetComponent<Fighter4>();
        impacted = false;
    }

    public void GetHit(int damage)
    {

        if (Health <= 0)
        {
            Health = 0;
        }
        Health -= damage;
    }

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        if (!isDead())
        {
            if (inRange())
            {
                chase();
            }
            else
            {
                SetIdle();
            }
        }
        else
        {

            if (firstTimeDed == 0)
            {
                Die();
                firstTimeDed += 1;
            }
        }
        Impact();

    }
    void Impact()
    {
        int atacaoare = animator.GetInteger("moving");
        if (opponent != null && atacaoare == 3 && !impacted)
        {
            atacaoare = animator.GetInteger("moving");
            Debug.Log("Animator state is attack and impacted is false");
            Debug.Log("Impact time reached, calling opponent.GetHit(damage)");
            opponent.GetHit(damage);
            impacted = true;
        }
        if (animator.GetInteger("moving") != 3)
        {
            impacted = false;
        }
    }

    bool inRange()
    {
        return Vector3.Distance(transform.position, player.position) < range;
    }

    void chase()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 0.3f * range)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= 0.3f * range && distanceToPlayer > 0.2 * range)
        {
            MoveWithinRange();

        }
        else
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
            else
            {
                SetIdle();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = player.position;
        Vector3 lookDir = transform.position - targetPosition;
        lookDir.y = 0; // Nu ne interesează direcția pe y
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f); // Rotim personajul către direcția opusă a jucătorului
        Vector3 moveDirection = targetPosition - transform.position;
        moveDirection.y = 0; // Nu ne interesează mișcarea pe y
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
        animator.SetInteger("moving", 2); // Setăm la run
    }

    void MoveWithinRange()
    {
        Vector3 targetPosition = player.position;
        Vector3 lookDir = transform.position - targetPosition;
        lookDir.y = 0; // Nu ne interesează direcția pe y
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * 360f); // Rotim personajul către direcția opusă a jucătorului
        Vector3 moveDirection = targetPosition - transform.position;
        moveDirection.y = 0; // Nu ne interesează mișcarea pe y
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
        animator.SetInteger("moving", 2); // Setăm la run
    }

    void SetIdle()
    {
        animator.SetInteger("moving", 0); // Setăm la idle
    }

    void Die()
    {
        animator.SetInteger("moving", 9); // Setăm la death
    }

    bool isDead()
    {
        return Health <= 0;
    }



    public void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > ImpactTime)
        {
            animator.SetInteger("moving", 3); // Setăm la attack
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                impacted = false;
            }
        }
    }
}
