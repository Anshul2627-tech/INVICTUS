using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        if (uiManager != null)
            uiManager.ShowGameOver();
        
        Debug.Log("GAME OVER!");
        Time.timeScale = 0f; // game pause ho jayega
    }
}