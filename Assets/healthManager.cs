using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    public Image healthBar; // Referința către bara de viață
    public float healthAmount = 100; // Cantitatea inițială de viață

    void Start()
    {
        UpdateHealthBar(); // Inițializăm bara de viață la început
    }

    // Metodă pentru actualizarea barei de viață
    public void UpdateHealthBar()
    {
        // Normalizăm valoarea vieții între 0 și 1 pentru a o afișa corect pe bara de viață
        healthBar.fillAmount = healthAmount / 100f;
    }

    // Metodă pentru primirea daunelor
    public void TakeDamage(float damage)
    {
        healthAmount -= damage; // Scădem daunele primite din viața totală

        // Verificăm dacă viața a ajuns la zero
        if (healthAmount <= 0)
        {
            healthAmount = 0; // Asigurăm că viața nu poate fi sub zero
            Die(); // Apelăm metoda Die pentru a gestiona moartea personajului
        }

        UpdateHealthBar(); // Actualizăm bara de viață
    }

    // Metodă pentru vindecare
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount; // Adăugăm cantitatea de vindecare la viața totală
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Limităm viața între 0 și 100

        UpdateHealthBar(); // Actualizăm bara de viață
    }

    // Metodă pentru gestionarea morții personajului
    void Die()
    {

    }

    // Metodă pentru resetarea barei de viață la 100%
    public void ResetHealth()
    {
        healthAmount = 100; // Resetăm sănătatea la 100
        UpdateHealthBar(); // Actualizăm bara de viață
    }
}
