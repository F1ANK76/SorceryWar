using System.Collections;
using System.Threading;
using UnityEngine;

public class SkillBSpawner : MonoBehaviour
{
    public GameObject laserPrefab;    // �ð��� ������
    public Vector3 spawnPos = new Vector3(0, 0, 0);
    public float spawnInterval = 3f;
    public float laserDuration = 1f;
    public int laserDamage = 5;

    public SkillManager skillManager; // SkillManager ����
    private bool spawning = false;
    void Update()
    {
        if (!spawning && skillManager != null && skillManager.skills.Length > 1 && skillManager.skills[1].level >= 1)
        {
            StartCoroutine(SpawnRoutine());
            spawning = true; // �ߺ� ���� ����
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
            // ��ų ������ŭ �߰� ������
            int skillLevel = 0;
            if (skillManager != null && skillManager.skills.Length > 1)
                skillLevel = skillManager.skills[1].level;

            float totalDamage = laserDamage + skillLevel;
            m.TakeDamage(totalDamage);
        }
    }
}
