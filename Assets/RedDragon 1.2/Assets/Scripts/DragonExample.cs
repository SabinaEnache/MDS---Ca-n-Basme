using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonExample : MonoBehaviour, IEnemyController
{
    public float speed;
    public float range;
    public CharacterController controller;
    public Transform player;
    public int firstTimeDed = 0;

    public float ImpactTime = 0.71f;
    public int Health;
    public int damage;
    private Animator animator;
    private float attackCooldown = 2.0f; // Cooldown de atac de 2 secunde
    private float lastAttackTime = -2.0f; // Timpul ultimei atacuri

    // Hashes pentru animații
    private int Walk;
    private int BattleStance;
    private int Bite;
    private int Die;
    private bool impacted;

    private Fighter4 opponent;

    void Start()
    {
        Health = 100;
        animator = GetComponent<Animator>();
        Walk = Animator.StringToHash("Walk");
        BattleStance = Animator.StringToHash("BattleStance");
        Bite = Animator.StringToHash("Bite");
        Die = Animator.StringToHash("Die");
        opponent=player.GetComponent<Fighter4>();
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
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f) {
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
                DieAnimation();
                firstTimeDed += 1;
            }
            else
            {

            }
        }
        Impact();
    }
    void Impact()
    {
        bool atacaoare = animator.GetBool(Bite);
        if (opponent != null && atacaoare == true && !impacted)
        {
            atacaoare = animator.GetBool(Bite);
            Debug.Log("Animator state is attack and impacted is false");
            Debug.Log("Impact time reached, calling opponent.GetHit(damage)");
            opponent.GetHit(damage);
            impacted = true;
        }
        if (animator.GetBool(Bite) == false)
        {
            impacted = false;
        }
    }

    bool inRange()
    {
        if(player != null)
            return Vector3.Distance(transform.position, player.position) < range;
        return false;
    }

    void chase()
    {
        if(player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 0.5f * range)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer <= 0.5f * range && distanceToPlayer > 0.24f * range)
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
        if(player == null)
            return;

        transform.LookAt(player.position);
        Vector3 moveDirection = player.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;

        // Setăm animațiile corespunzătoare
        animator.SetBool(BattleStance, false);
        animator.SetBool(Bite, false);
        animator.SetBool(Walk, true);
        animator.SetBool(Die, false);
    }

    void MoveWithinRange()
    {
        if(player == null)
            return;

        transform.LookAt(player.position);
        Vector3 targetPosition = player.position - transform.forward * 0.1f * range;
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.y = 0;
            moveDirection.Normalize();
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        // Setăm animațiile corespunzătoare
        animator.SetBool(BattleStance, true);
        animator.SetBool(Bite, false);
        animator.SetBool(Walk, false);
        animator.SetBool(Die, false);
    }

    void SetIdle()
    {
        // Setăm animațiile corespunzătoare
        animator.SetBool(BattleStance, true);
        animator.SetBool(Bite, false);
        animator.SetBool(Walk, false);
        animator.SetBool(Die, false);
    }

    void DieAnimation()
    {
        // Setăm animațiile corespunzătoare
        animator.SetBool(BattleStance, false);
        animator.SetBool(Bite, false);
        animator.SetBool(Walk, false);
        animator.SetBool(Die, true);
    }

    bool isDead()
    {
        return Health <= 0;
    }


    public void Attack()
    {
        if(player == null)
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > ImpactTime)
        {
            animator.SetBool(BattleStance, false);
            animator.SetBool(Bite, true);
            animator.SetBool(Walk, false);
            animator.SetBool(Die, false);
            impacted = false; // Resetăm impacted la începutul atacului
        }
    }
}
