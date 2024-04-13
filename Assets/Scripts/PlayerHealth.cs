using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    private int maxHealth { get; set; } = 3;
    
    private int currentHealth { get; set; } = 3;

    private bool takingDamage = false;

    private BoxCollider2D triggerCollider;

    private SpriteRenderer playerSprite;

    void Update()
    {
        iFrames();
        CheckDeath();
    }

    // damage/heal handling below
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            takingDamage = true;
            TakeDamage(1);
        }
    }

    private void iFrames()
    {
        if (takingDamage)
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        int i = 0;
        triggerCollider.enabled = false;
        while (i < nFlashes)
        {
            playerSprite.Color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            i++;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Heal(int health)
    {
        currentHealth += health;
    }

    // resets health (use after death etc.)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    // death handling below
    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
