using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Hearts")]
    public GameObject[] hearts; // Assign 3 heart GameObjects in order (left to right)

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    public ParallaxController parallaxController;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        GameManager.Instance.isGameRunning = false;
        UIController.Instance.EnableGameLoseCanvas();
        UIController.Instance.UpdateDistance(parallaxController.distance);
        gameObject.SetActive(false);
    }
}