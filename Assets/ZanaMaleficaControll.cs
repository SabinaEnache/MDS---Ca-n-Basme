// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ZanaMaleficaControl : MonoBehaviour, IEnemyController
// {
//     public float speed;
//     public float range;
//     public int firstTimeDed = 0;
//     public CharacterController controller;
//     public Transform player;
//     private Fighter4 opponent;
//     private bool impacted;
//     public float ImpactTime = 0.36f;
//     public int Health;
//     public int damage;
//     private Animation anim;
//     public AnimationClip Run;
//     public AnimationClip Idle;
//     public AnimationClip Attack1;
//     public AnimationClip Death;
//     private float attackCooldown = 2.0f; // Cooldown de atac de 2 secunde
//     private float lastAttackTime = -2.0f; // Timpul ultimei atacuri
//     private bool isDeadFlag = false; // Steag pentru a verifica dacă animația de moarte a fost jucată

//     void Start()
//     {
//         Health = 100;
//         opponent = player.GetComponent<Fighter4>();
//         impacted = false;

//         // Adăugare animații la componenta Animation
//         if (anim != null)
//         {
//             anim.AddClip(Run, "Run");
//             anim.AddClip(Idle, "Idle");
//             anim.AddClip(Attack1, "Attack1");
//             anim.AddClip(Death, "Death");
//         }
//         else
//         {
//             Debug.LogError("Animation component missing on this GameObject.");
//         }
//     }

//     public void GetHit(int damage)
//     {
//         Health -= damage;
//         if (Health <= 0)
//         {
//             Health = 0;
//             isDeadFlag = true;
//             Debug.Log("Enemy is dead.");
//         }
//         else
//         {
//             Debug.Log("Enemy took damage, remaining health: " + Health);
//         }
//     }

//     void Update()
//     {
//         if (!isDead())
//         {
//             if (inRange())
//             {
//                 chase();
//             }
//             else
//             {
//                 SetIdle();
//             }
//         }
//         else if (isDeadFlag)
//         {
//             if (firstTimeDed == 0)
//             {
//                 DieAnimation();
//                 isDeadFlag = false;
//                 firstTimeDed += 1;
//             }
//         }
//         Impact();
//     }

//     void Impact()
//     {
//         // Verificăm dacă animația de atac este rulată și dacă nu am aplicat încă impactul
//         if (opponent != null && anim.IsPlaying("Attack1") && !impacted)
//         {
//             Debug.Log("Animator state is attack and impacted is false");
//             if (anim["Attack1"].time > ImpactTime)
//             {
//                 Debug.Log("Impact time reached, calling opponent.GetHit(damage)");
//                 opponent.GetHit(damage);
//                 impacted = true;
//             }
//         }

//         // Resetăm impacted dacă animația de atac nu este rulată
//         if (!anim.IsPlaying("Attack1"))
//         {
//             impacted = false;
//         }
//     }

//     bool inRange()
//     {
//         return Vector3.Distance(transform.position, player.position) < range;
//     }

//     void chase()
//     {
//         float distanceToPlayer = Vector3.Distance(transform.position, player.position);
//         if (distanceToPlayer > 0.3f * range)
//         {
//             MoveTowardsPlayer();
//         }
//         else if (distanceToPlayer <= 0.3f * range && distanceToPlayer > 0.25     * range)
//         {
//             MoveWithinRange();
//         }
//         else
//         {
//             if (Time.time - lastAttackTime >= attackCooldown)
//             {
//                 Attack();
//                 lastAttackTime = Time.time;
//             }
//             else
//             {
//                 SetIdle();
//             }
//         }
//     }

//     void MoveTowardsPlayer()
//     {
//         transform.LookAt(player.position);
//         Vector3 moveDirection = player.position - transform.position;
//         moveDirection.y = 0;
//         moveDirection.Normalize();
//         transform.position += moveDirection * speed * Time.deltaTime;

//         // Rulează animația corespunzătoare
//         if (anim != null)
//         {
//             if (!anim.IsPlaying("Run"))
//             {
//                 Debug.Log("Playing Run animation");
//                 anim.Stop("Idle");
//                 anim.Stop("Attack1");
//                 anim.Stop("Death");
//                 anim.CrossFade("Run");
//             }
//         }
//     }

//     void MoveWithinRange()
//     {
//         transform.LookAt(player.position);
//         Vector3 targetPosition = player.position - transform.forward * 0.1f * range;
//         if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
//         {
//             Vector3 moveDirection = targetPosition - transform.position;
//             moveDirection.y = 0;
//             moveDirection.Normalize();
//             transform.position += moveDirection * speed * Time.deltaTime;

//             // Rulează animația corespunzătoare
//             if (anim != null)
//             {
//                 if (!anim.IsPlaying("Idle"))
//                 {
//                     Debug.Log("Playing Idle animation");
//                     anim.Stop("Run");
//                     anim.Stop("Attack1");
//                     anim.Stop("Death");
//                     anim.CrossFade("Idle");
//                 }
//             }
//         }
//         else
//         {
//             if (Time.time - lastAttackTime >= attackCooldown)
//             {
//                 Attack();
//                 lastAttackTime = Time.time;
//             }
//             else
//             {
//                 SetIdle();
//             }
//         }
//     }

//     void SetIdle()
//     {
//         if (anim != null)
//         {
//             if (!anim.IsPlaying("Idle"))
//             {
//                 Debug.Log("Setting Idle animation");
//                 anim.Stop("Run");
//                 anim.Stop("Attack1");
//                 anim.Stop("Death");
//                 anim.CrossFade("Idle");
//             }
//         }
//     }

//     void DieAnimation()
//     {
//         if (anim != null)
//         {
//             if (!anim.IsPlaying("Death"))
//             {
//                 Debug.Log("Playing Death animation");
//                 anim.Stop("Run");
//                 anim.Stop("Idle");
//                 anim.Stop("Attack1");
//                 anim.CrossFade("Death");
//             }
//         }
//     }

//     bool isDead()
//     {
//         return Health <= 0;
//     }

//     public void Attack()
//     {
//         if (anim != null)
//         {
//             Debug.Log("Attack initiated");
//             anim.Stop("Run");
//             anim.Stop("Idle");
//             anim.Stop("Death");
//             anim.CrossFade("Attack1");
//             impacted = false;
//         }
//     }
// }
