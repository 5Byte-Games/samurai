using System.Collections;
using UnityEngine;
using TMPro;

public class playerHealth : MonoBehaviour
{
    private int maxHealth { get; set; } = 3;
    
    private int currentHealth { get; set; } = 3;

    private bool takingDamage = false;

    public Collider2D triggerCollider;

    public SpriteRenderer playerSprite;

    public TextMeshProUGUI healthText;

    [SerializeField] private Color flashingColor;
    [SerializeField] private Color originalColor;
    [SerializeField] private float flashDuration;
    [SerializeField] private int nFlashes;


    void Update()
    {
        CheckDeath();
    }

    // damage/heal handling below
    private void OnCollisionStay2D(Collision2D collision)
    {  
        if(takingDamage) return;
        if (collision.gameObject.CompareTag("Hazard"))
        {
            takingDamage = true;
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(iFrames());

        healthText.text = "Health: " + currentHealth.ToString();
    }

    // gives player iFrames and flashes colors
    private IEnumerator iFrames()
    {
        triggerCollider.enabled = false;
        for (int i = 0; i < nFlashes; i++)
        {
            i++;
            playerSprite.color = flashingColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
        triggerCollider.enabled = true;
        takingDamage = false;
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
