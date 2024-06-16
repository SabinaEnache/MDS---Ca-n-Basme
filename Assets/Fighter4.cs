using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEnemyController
{
    void GetHit(int damage);
}

public class Fighter4 : MonoBehaviour
{
    public List<GameObject> opponents; // Lista de oponenți
    public int damage = 40;
    public int range = 4;
    public int health;
    private int MaxHealth;
    public double ImpactTime = 0.63;
    private bool AttackTriggered = false;
    private Animator anim;
    private float lastAttackTime; // timpul ultimului atac
    public bool impacted;
    private GameObject currentOpponent; // Oponentul curent selectat
    public static bool IsDead { get; private set; } = false; // Proprietate statică pentru starea de viață a personajului

    void Start()
    {
        anim = GetComponent<Animator>();
        Movement.Attack = false; // Asigură-te că starea inițială nu este de atac
        lastAttackTime = Time.time; // Initializează timpul ultimului atac
        impacted = false;
    }

    void Update()
    {
        if (IsDead) return;

        currentOpponent = GetClosestOpponent(); // Selectează cel mai apropiat oponent

        if (currentOpponent != null && Input.GetMouseButtonDown(0) && !Movement.Attack && !AttackTriggered && inRange())
        {
            // Verifică dacă a trecut suficient timp de la ultimul atac
            if (Time.time - lastAttackTime >= 0.20f)
            {
                // Verifică dacă caracterul este în Idle pentru a evita suprapunerea animațiilor
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    anim.SetBool("Idle", false); // Oprește orice animație care rulează pentru a evita suprapunerea
                }

                anim.SetBool("IsPunchRight", true);
                StartCoroutine(ResetAttackState(anim.GetCurrentAnimatorStateInfo(0).length));
                AttackTriggered = true;
                Movement.Attack = true;
                lastAttackTime = Time.time; // Actualizează timpul ultimului atac
            }
        }
        Impact();

        Died();
    }

    IEnumerator ResetAttackState(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Movement.Attack = false;
        AttackTriggered = false;
        impacted = false;
        anim.SetBool("IsPunchRight", false);
    }

    public void GetHit(int damage)
    {
        Debug.Log("A luat hit player-ul");
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    void Impact()
    {
        if (currentOpponent != null && anim.GetCurrentAnimatorStateInfo(0).IsName("PunchRight") && !impacted)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > ImpactTime)
            {
                IEnemyController enemyController = currentOpponent.GetComponent<IEnemyController>();
                if (enemyController != null)
                {
                    enemyController.GetHit(damage);
                    impacted = true;
                }
            }
        }
    }

    bool inRange()
    {
        if (currentOpponent != null && Vector3.Distance(currentOpponent.transform.position, transform.position) <= range)
        {
            Debug.Log("Opponent in range.");
            return true;
        }
        else
        {
            Debug.Log("Opponent out of range.");
            return false;
        }
    }

    GameObject GetClosestOpponent()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;
        foreach (GameObject opponent in opponents)
        {
            if (opponent != null) // Verificăm dacă opponent-ul nu a fost distrus
            {
                float distance = Vector3.Distance(opponent.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = opponent;
                }
            }
        }
        return closest;
    }

    void Died()
    {
        if (health <= 0)
        {
            if (!IsDead)
            {
                anim.SetBool("IsDeath", true);
                IsDead = true; // Setează IsDead la true când personajul moare
                StartCoroutine(Respawn()); // Pornește corutina pentru respawn
                Debug.Log("Character died, starting respawn...");
            }
        }
    }

    IEnumerator Respawn()
    {
        Debug.Log("Respawn coroutine started...");
        yield return new WaitForSeconds(5); // Așteaptă 2 secunde pentru respawn
        Debug.Log("Respawning...");

        // Resetează sănătatea și poziția personajului
        health = MaxHealth;

        // Dezactivează CharacterController temporar pentru a evita interferențele la setarea poziției
        GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(17, 0.115f, 19);

        // Resetează toate stările animatorului
        anim.SetBool("IsDeath", false);
        anim.Play("Idle", 0); // Joacă animația "Idle"

        // Re-activăm CharacterController
        GetComponent<CharacterController>().enabled = true;

        IsDead = false; // Resetează starea de viață a personajului
        Debug.Log("Respawn complete. Health restored to " + health);
    }
}
