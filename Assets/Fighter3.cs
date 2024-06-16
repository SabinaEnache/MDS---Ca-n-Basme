// using UnityEngine;
// using System.Collections;

// public class Fighter3 : MonoBehaviour
// {
//     public GameObject opponent;
//     public int damage = 40;
//     public double ImpactTime = 0.63;
//     private bool AttackTriggered = false;
//     private Animation animation;
//     public bool impacted;

//     void Start()
//     {
//         animation = GetComponent<Animation>();
//         Movement.Attack = false;  // Asigură-te că starea inițială nu este de atac
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space) && !WASDToMove.Attack && !AttackTriggered)
//         {
//             // Verifică dacă caracterul este în Idle pentru a evita suprapunerea animațiilor
//             if (animation.IsPlaying("Idle"))
//             {
//                 animation.Stop(); // Oprește orice animație care rulează pentru a evita suprapunerea
//             }

//             animation.Play("PunchRight");
//             AttackTriggered = true;
//             StartCoroutine(ResetAttackState(animation["PunchRight"].length));
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
//         impacted=false;
//     }

//     void Impact()
//     {
//         if (opponent != null && animation != null && animation.IsPlaying("PunchRight") && !impacted)
//         {
//             if (animation["PunchRight"].time > animation["PunchRight"].length * (float)ImpactTime)
//             {
//                 opponent.GetComponent<Mob>().GetHit(damage);
//                 impacted = true;
//             }
//         }
//     }
// }
