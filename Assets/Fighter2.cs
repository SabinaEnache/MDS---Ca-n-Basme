// using UnityEngine;
// using System.Collections;

// public class Fighter : MonoBehaviour
// {
//     public GameObject opponent;
//     public AnimationClip PunchRight; // Punch Right
//     private bool AttackTriggered = false;

//     public int damage = 40;
//     public double ImpactTime = 0.63;
//     public bool impacted;

//     void Start()
//     {
//         WASDToMove.Attack = false;  // Asigură-te că starea inițială nu este de atac

//         // Asigură-te că animația de atac este adăugată la componenta Animation
//         var animation = GetComponent<Animation>();
//         if (animation != null)
//         {
//             animation.AddClip(PunchRight, PunchRight.name);
//         }
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && !WASDToMove.Attack && !AttackTriggered)
//         {
//             var animation = GetComponent<Animation>();
//             if (animation != null)
//             {
//                 animation.CrossFade(PunchRight.name);
//             }
//             WASDToMove.Attack = true;
//             AttackTriggered = true;
//             StartCoroutine(ResetAttackState(PunchRight.length));  // Start Coroutine to reset attack state after animation duration
//             if (opponent != null)
//             {
//                 transform.LookAt(opponent.transform.position);
//                 opponent.GetComponent<Mob>().GetHit(damage);
//             }
//         }
//         Impact();
//     }

//     IEnumerator ResetAttackState(float waitTime)
//     {
//         yield return new WaitForSeconds(waitTime);
//         WASDToMove.Attack = false;
//         AttackTriggered = false;
//         impacted = false;
//     }

//     void Impact()
//     {
//         var animation = GetComponent<Animation>();
//         if (opponent != null && animation != null && animation.IsPlaying(PunchRight.name) && !impacted)
//         {
//             if (animation[PunchRight.name].time > animation[PunchRight.name].length * (float)ImpactTime)
//             {
//                 opponent.GetComponent<Mob>().GetHit(damage);
//                 impacted = true;
//             }
//         }
//     }
// }
