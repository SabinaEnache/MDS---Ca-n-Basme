using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolControl : MonoBehaviour, IEnemyController
{
    public float speed;
    public float range;
    public int firstTimeDed=0;
    public CharacterController controller;
    public Transform player;

    public float RangeDeAttack=1;

    public float ImpactTime=0.70f;
    public int Health;
    public int damage;
    private Animator animator;
    private float attackCooldown = 2.0f; // Cooldown de atac de 2 secunde
    private float lastAttackTime = -2.0f; // Timpul ultimei atacuri
    private bool impacted;
    private Fighter4 opponent;


    void Start()
    {
        Health = 100;
        animator = GetComponent<Animator>();
        opponent=player.GetComponent<Fighter4>();
        impacted=false;
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
        if(animator.GetInteger("moving")==9 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f) {
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

            if(firstTimeDed==0){
                Die();
                firstTimeDed+=1;
            }
        }
        Impact();

    }
void Impact()
{
    int atacaoare=animator.GetInteger("moving");
    if (opponent != null && atacaoare == 3 && !impacted)
    {
        atacaoare=animator.GetInteger("moving");
        Debug.Log("Animator state is attack and impacted is false");
            Debug.Log("Impact time reached, calling opponent.GetHit(damage)");
            opponent.GetHit(damage);
            impacted = true;
    }
    if (animator.GetInteger("moving")!=3)
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
        else if (distanceToPlayer <= 0.3f * range && distanceToPlayer > 0.2*range)
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
        transform.LookAt(player.position);
        Vector3 moveDirection = player.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
        animator.SetInteger("moving", 2); // Setăm la run
    }

    void MoveWithinRange()
    {
        transform.LookAt(player.position);
        Vector3 targetPosition = player.position - transform.forward * 0.1f * range;
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.y = 0;
            moveDirection.Normalize();
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        animator.SetInteger("moving", 0); // Setăm la idle
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
        impacted = false; // Resetăm impacted la începutul atacului
        }
    }
}
