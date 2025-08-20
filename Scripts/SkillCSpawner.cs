using System.Collections;
using UnityEngine;

public class SkillCSpawner : MonoBehaviour
{
    public GameObject minePrefab;        // ���� ������
    public float spawnInterval = 6f;     // ���� ��ȯ �ֱ�
    public float mineDuration = 3f;      // ���� ���� �ð�
    public float damageInterval = 1f;    // �浹 ������ ���� ����
    public int mineDamage = 15;          // ������

    // ���� ��ȯ ��ġ
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(0, 1, 0),
        new Vector3(0, -1, 0),
        new Vector3(0, -3, 0)
    };

    public SkillManager skillManager; // SkillManager ����
    private bool spawning = false;
    void Update()
    {
        if (!spawning && skillManager != null && skillManager.skills.Length > 2 && skillManager.skills[2].level >= 1)
        {
            StartCoroutine(SpawnRoutine());
            spawning = true; // �ߺ� ���� ����
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnMines();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMines()
    {
        foreach (Vector3 pos in spawnPositions)
        {
            GameObject mine = Instantiate(minePrefab, pos, Quaternion.identity);

            // ���� �浹 ó�� ��ũ��Ʈ �߰�
            MineBehavior behavior = mine.AddComponent<MineBehavior>();
            behavior.damage = mineDamage;
            behavior.damageInterval = damageInterval;
            behavior.skillManager = skillManager; // �� ���⼭ �Ҵ�

            // ���� ���� �ð� �� ����
            Destroy(mine, mineDuration);
        }
    }
}

// -------------------------------
// ���� �浹 ó��
// -------------------------------
public class MineBehavior : MonoBehaviour
{
    public int damage;
    public float damageInterval;
    public SkillManager skillManager; // ���� �߰�

    private float lastDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Monster monster = other.GetComponent<Monster>();

        if (monster != null)
        {
            // ������ ���� üũ
            if (Time.time - lastDamageTime >= damageInterval)
            {
                // ��ų ������ŭ �߰� ������
                int skillLevel = 0;
                if (skillManager != null && skillManager.skills.Length > 2)
                    skillLevel = skillManager.skills[2].level;

                float totalDamage = damage + skillLevel;
                monster.TakeDamage(totalDamage);

                lastDamageTime = Time.time;
            }
        }
    }
}
