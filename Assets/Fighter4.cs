using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
	public int MaxHealth = 100;
	public double ImpactTime = 0.63;
	private bool AttackTriggered = false;
	private Animator anim;
	private healthManager healthManageru; // Modificare aici pentru a asigura inițializarea

	private float lastAttackTime; // timpul ultimului atac
	public bool impacted;
	private GameObject currentOpponent; // Oponentul curent selectat
	public static bool IsDead { get; private set; } = false; // Proprietate statică pentru starea de viață a personajului

	private int XP = 0;
	private int level = 1;
	private int xpToNextLevel = 6;

	void Start()
	{
		anim = GetComponent<Animator>();
		Movement.Attack = false; // Asigură-te că starea inițială nu este de atac
		lastAttackTime = Time.time; // Initializează timpul ultimului atac
		impacted = false;

		// Inițializează healthManageru
		healthManageru = GameObject.Find("healthManager").GetComponent<healthManager>(); // Modificare aici
		if (healthManageru == null)
		{
			UnityEngine.Debug.LogError("healthManageru is not set. Make sure to assign it in the Inspector.");
		}
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
		UnityEngine.Debug.Log("A luat hit player-ul");
		health -= damage;
		if (health < 0)
		{
			health = 0;
		}
		if (healthManageru != null) // Verifică dacă healthManageru este inițializat
		{
			healthManageru.TakeDamage(damage);
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
					if (IsEnemyDead(currentOpponent))
					{
						UnityEngine.Debug.Log("You gained 4 XP!");
						CheckForLevelUp();
					}
				}
			}
		}
	}
	bool IsEnemyDead(GameObject enemy)
	{
		// Verificăm dacă inamicul este mort
		if (enemy != null)
		{
			TrolControl trol = enemy.GetComponent<TrolControl>();
			if (trol != null)
			{
				return trol.Health <= 0;
			}
			DragonExample dragon = enemy.GetComponent<DragonExample>();
			if (dragon != null)
			{
				return dragon.Health <= 0;
			}
		}
		return false;
	}
	void CheckForLevelUp()
	{
		if (XP >= xpToNextLevel)
		{
			LevelUp();
		}
	}

	void LevelUp()
	{
		level++; // Incrementez nivelul

		// Adaug un bonus de 10% damage și 10% health la fiecare level up
		int damageBonus = Mathf.RoundToInt(damage * 0.1f);
		int healthBonus = Mathf.RoundToInt(MaxHealth * 0.1f);

		damage += damageBonus;
		MaxHealth += healthBonus;
		health += healthBonus;

		// Reset XP și calculez XP-ul necesar pentru următorul level
		XP = 0;
		xpToNextLevel += 6;

		UnityEngine.Debug.Log("Level up! You are now level " + level + ".");
		UnityEngine.Debug.Log("Damage increased by 10%.");
		UnityEngine.Debug.Log("Health increased by 10%.");
	}

	bool inRange()
	{
		if (currentOpponent != null && Vector3.Distance(currentOpponent.transform.position, transform.position) <= range)
		{
			UnityEngine.Debug.Log("Opponent in range.");
			return true;
		}
		else
		{
			UnityEngine.Debug.Log("Opponent out of range.");
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
				UnityEngine.Debug.Log("Character died, starting respawn...");
			}
			if (healthManageru != null)
			{
				healthManageru.ResetHealth(); // Resetăm bara de viață
			}
		}
	}

	IEnumerator Respawn()
	{
		UnityEngine.Debug.Log("Respawn coroutine started...");
		yield return new WaitForSeconds(5); // Așteaptă 5 secunde pentru respawn
		UnityEngine.Debug.Log("Respawning...");

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
		UnityEngine.Debug.Log("Respawn complete. Health restored to " + health);
	}
}