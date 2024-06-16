using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZanaMalefica : MonoBehaviour, IEnemyController
{
    public float speed;
    public float range;
    public int firstTimeDed = 0;
    public CharacterController controller;
    public Transform player;
    private Fighter4 opponent;
    private bool impacted;
    public float ImpactTime = 0.36f;
    public int Health;
    public int damage;
    private Animation anim;
    public AnimationClip Witch_skeleton_walk;
    public AnimationClip Witch_skeleton_idle;
    public AnimationClip Witch_skeleton_attack;
    public AnimationClip Witch_skeleton_death;
    private float attackCooldown = 2.0f; // Cooldown de atac de 2 secunde
    private float lastAttackTime = -2.0f; // Timpul ultimei atacuri
    private bool isDeadFlag = false; // Steag pentru a verifica dacă animația de moarte a fost jucată

    void Start()
    {
        Health = 100;
        opponent = player.GetComponent<Fighter4>();
        impacted = false;

        anim = GetComponent<Animation>();

        // Adăugare animații la componenta Animation
        if (anim != null)
        {
            anim.AddClip(Witch_skeleton_walk, "Witch.Skeleton|walk");
            anim.AddClip(Witch_skeleton_idle, "Witch.Skeleton|idle");
            anim.AddClip(Witch_skeleton_attack, "Witch.Skeleton|attack");
            anim.AddClip(Witch_skeleton_death, "Witch.Skeleton|death");
        }
        else
        {
            Debug.LogError("Animation component missing on this GameObject.");
        }
    }

    public void GetHit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            isDeadFlag = true;
            Debug.Log("Enemy is dead.");
        }
        else
        {
            Debug.Log("Enemy took damage, remaining health: " + Health);
        }
    }

    void Update()
    {
        if (!isDead())
        {
            if (inRange())
            {
                chase();
            }
            else
            {
                SetIdleAnimation();
            }
        }
        else if (isDeadFlag)
        {
            if (firstTimeDed == 0)
            {
                DieAnimation();
                isDeadFlag = false;
                firstTimeDed += 1;
            }
        }
        Impact();
    }

    void Impact()
    {
        // Verificăm dacă animația de atac este rulată și dacă nu am aplicat încă impactul
        if (opponent != null && anim.IsPlaying("Witch.skeleton_attack") && !impacted)
        {
            Debug.Log("Animator state is attack and impacted is false");
            if (anim["Witch.skeleton_attack"].time > ImpactTime)
            {
                Debug.Log("Impact time reached, calling opponent.GetHit(damage)");
                opponent.GetHit(damage);
                impacted = true;
            }
        }

        // Resetăm impacted dacă animația de atac nu este rulată
        if (!anim.IsPlaying("Witch.skeleton_attack"))
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
        else if (distanceToPlayer <= 0.3f * range && distanceToPlayer > 0.25 * range)
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
                SetIdleAnimation();
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

        // Rulează animația corespunzătoare
        if (anim != null)
        {
            if (!anim.IsPlaying("Witch.skeleton_walk"))
            {
                Debug.Log("Playing walk animation");
                anim.Stop("Witch.skeleton_idle");
                anim.Stop("Witch.skeleton_attack");
                anim.Stop("Witch.skeleton_death");
                anim.CrossFade("Witch.skeleton_walk");
            }
        }
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

            // Rulează animația corespunzătoare
            if (anim != null)
            {
                if (!anim.IsPlaying("Witch.skeleton_idle"))
                {
                    Debug.Log("Playing idle animation");
                    anim.Stop("Witch.skeleton_walk");
                    anim.Stop("Witch.skeleton_attack");
                    anim.Stop("Witch.skeleton_death");
                    anim.CrossFade("Witch.skeleton_idle");
                }
            }
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
                SetIdleAnimation();
            }
        }
    }

    void SetIdleAnimation()
    {
        if (anim != null)
        {
            if (!anim.IsPlaying("Witch.skeleton_idle"))
            {
                Debug.Log("Setting idle animation");
                anim.Stop("Witch.skeleton_walk");
                anim.Stop("Witch.skeleton_attack");
                anim.Stop("Witch.skeleton_death");
                anim.CrossFade("Witch.skeleton_idle");
            }
        }
    }

    void DieAnimation()
    {
        if (anim != null)
        {
            if (!anim.IsPlaying("Witch.skeleton_death"))
            {
                Debug.Log("Playing Death animation");
                anim.Stop("Witch.skeleton_walk");
                anim.Stop("Witch.skeleton_idle");
                anim.Stop("Witch.skeleton_attack");
                anim.CrossFade("Witch.skeleton_death");
            }
        }
    }

    bool isDead()
    {
        return Health <= 0;
    }

    public void Attack()
    {
        if (anim != null)
        {
            Debug.Log("Attack initiated");
            anim.Stop("Witch.skeleton_walk");
            anim.Stop("Witch.skeleton_idle");
            anim.Stop("Witch.skeleton_death");
            anim.CrossFade("Witch.skeleton_attack");
            impacted = false;
        }
    }
}
