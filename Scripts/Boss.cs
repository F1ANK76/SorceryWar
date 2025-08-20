using UnityEngine;
using System.Collections;

public class Boss : Monster
{
    private bool hasReachedPlayer = false;
    private Player targetPlayer;

    void Start()
    {
        Player player = FindObjectOfType<Player>();

        // 씬에서 WaveManager 찾아오기
        WaveManager waveManager = FindObjectOfType<WaveManager>();

        if (waveManager != null)
            waveNumber = waveManager.currentWave;
        else Debug.Log("wave null");

        currentHealth = baseHealth + (waveNumber * 100);
    }

    void Update()
    {
        if (!hasReachedPlayer)
            transform.position += Vector3.up * 0.5f * Time.deltaTime;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);

            Player player = FindObjectOfType<Player>();

            if (player != null)
                player.AddGold(50);
        }

        healthBar.SetHealth(currentHealth, baseHealth + (waveNumber * 100));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasReachedPlayer = true;
            targetPlayer = other.GetComponent<Player>();

            if (targetPlayer != null)
                StartCoroutine(DamagePlayerLoop());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasReachedPlayer = false;
            targetPlayer = null; // 플레이어가 범위를 벗어나면 null 처리
        }
    }

    private IEnumerator DamagePlayerLoop()
    {
        while (hasReachedPlayer && targetPlayer != null)
        {
            targetPlayer.TakeDamage(5 + waveNumber);
            yield return new WaitForSeconds(1f);
        }
    }
}
