// using UnityEngine;
// using System.Collections;

// public class Fighter2 : MonoBehaviour
// {
//     public GameObject opponent;
//     public AnimationClip attack;
//     private bool AttackTriggered = false;

//     public int damage = 40;
//     public double ImpactTime = 0.63;
//     public bool impacted;

//     void Start()
//     {
//         WASDToMove.Attack = false;  // Asigură-te că starea inițială nu este de atac
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && !WASDToMove.Attack && !AttackTriggered)
//         {
//             GetComponent<Animation>().CrossFade(attack.name);
//             WASDToMove.Attack = true;
//             AttackTriggered = true;
//             StartCoroutine(ResetAttackState(attack.length));  // Start Coroutine to reset attack state after animation duration
//             if (opponent != null)
//             {
//                 transform.LookAt(opponent.transform.position);
//                 opponent.GetComponent<Mob>().GetHit(damage);
//             }
//         }
//         impact();
//     }

//     IEnumerator ResetAttackState(float waitTime)
//     {
//         yield return new WaitForSeconds(waitTime);
//         WASDToMove.Attack = false;
//         AttackTriggered = false;
//         impacted = false;
//     }

//     void impact()
//     {
//         var anim = GetComponent<Animation>();
//         if (opponent != null && anim.IsPlaying(attack.name) && !impacted)
//         {
//             if (anim[attack.name].time > anim[attack.name].length * (float)ImpactTime)
//             {
//                 opponent.GetComponent<Mob>().GetHit(damage);
//                 impacted = true;
//             }
//         }
//     }
// }
