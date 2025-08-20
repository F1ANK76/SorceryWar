using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;

    public int gold = 100; // 시작 골드
    public TextMeshProUGUI goldText; // Canvas에 연결

    public WaveManager waveManager; // WaveManager를 Inspector에서 연결

    void Start()
    {
        UpdateGoldUI();
        currentHealth = maxHealth;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
            goldText.text = gold + " G";
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            PlayerPrefs.SetInt("FinalWave", waveManager.currentWave - 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOverScene");
        }
            
        healthBar.SetHealth(currentHealth, maxHealth);
    }
}
