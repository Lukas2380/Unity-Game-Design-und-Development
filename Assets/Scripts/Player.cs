using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;

    public Image damageOverlay;
    public GameObject gameOverScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        
        damageOverlay.gameObject.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        ShowDamageOverlay();
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ShowDamageOverlay()
    {
        StartCoroutine(FlashOverlay());
    }

    private IEnumerator FlashOverlay()
    {
        damageOverlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damageOverlay.gameObject.SetActive(false);
    }
}