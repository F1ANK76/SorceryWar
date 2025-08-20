using System.Collections;
using System.Threading;
using UnityEngine;

public class SkillBSpawner : MonoBehaviour
{
    public GameObject laserPrefab;    // 시각용 레이저
    public Vector3 spawnPos = new Vector3(0, 0, 0);
    public float spawnInterval = 3f;
    public float laserDuration = 1f;
    public int laserDamage = 5;

    public SkillManager skillManager; // SkillManager 참조
    private bool spawning = false;
    void Update()
    {
        if (!spawning && skillManager != null && skillManager.skills.Length > 1 && skillManager.skills[1].level >= 1)
        {
            StartCoroutine(SpawnRoutine());
            spawning = true; // 중복 실행 방지
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnLaser();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnLaser()
    {
        if (laserPrefab != null)
        {
            GameObject laser = Instantiate(laserPrefab, spawnPos, Quaternion.identity);
            Destroy(laser, laserDuration);
        }

        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster m in monsters)
        {
            // 스킬 레벨만큼 추가 데미지
            int skillLevel = 0;
            if (skillManager != null && skillManager.skills.Length > 1)
                skillLevel = skillManager.skills[1].level;

            float totalDamage = laserDamage + skillLevel;
            m.TakeDamage(totalDamage);
        }
    }
}
