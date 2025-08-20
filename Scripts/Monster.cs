using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float baseHealth = 50f;
    public float currentHealth;
    public HealthBar healthBar;

    public int waveNumber = 1; // 웨이브 번호
    void Start()
    {
        // 씬에서 WaveManager 찾아오기
        WaveManager waveManager = FindObjectOfType<WaveManager>();

        if (waveManager != null)
            waveNumber = waveManager.currentWave;
        else Debug.Log("wave null");

        currentHealth = baseHealth + (waveNumber * 30);
    }

    void Update()
    {
        transform.position += Vector3.up * 0.5f * Time.deltaTime;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);

            Player player = FindObjectOfType<Player>();

            if (player != null)
                player.AddGold(3);
            else Debug.Log("player null");
        }
            
        healthBar.SetHealth(currentHealth, baseHealth + (waveNumber * 30));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(10f); // 예: 10 데미지
            }

            Destroy(gameObject);
        }
    }
}
