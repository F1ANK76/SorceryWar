using System.Collections;
using UnityEngine;

public class SkillCSpawner : MonoBehaviour
{
    public GameObject minePrefab;        // 지뢰 프리팹
    public float spawnInterval = 6f;     // 지뢰 소환 주기
    public float mineDuration = 3f;      // 지뢰 유지 시간
    public float damageInterval = 1f;    // 충돌 데미지 적용 간격
    public int mineDamage = 15;          // 데미지

    // 지뢰 소환 위치
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(0, 1, 0),
        new Vector3(0, -1, 0),
        new Vector3(0, -3, 0)
    };

    public SkillManager skillManager; // SkillManager 참조
    private bool spawning = false;
    void Update()
    {
        if (!spawning && skillManager != null && skillManager.skills.Length > 2 && skillManager.skills[2].level >= 1)
        {
            StartCoroutine(SpawnRoutine());
            spawning = true; // 중복 실행 방지
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

            // 지뢰 충돌 처리 스크립트 추가
            MineBehavior behavior = mine.AddComponent<MineBehavior>();
            behavior.damage = mineDamage;
            behavior.damageInterval = damageInterval;
            behavior.skillManager = skillManager; // ← 여기서 할당

            // 지뢰 유지 시간 후 삭제
            Destroy(mine, mineDuration);
        }
    }
}

// -------------------------------
// 지뢰 충돌 처리
// -------------------------------
public class MineBehavior : MonoBehaviour
{
    public int damage;
    public float damageInterval;
    public SkillManager skillManager; // 참조 추가

    private float lastDamageTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Monster monster = other.GetComponent<Monster>();

        if (monster != null)
        {
            // 데미지 간격 체크
            if (Time.time - lastDamageTime >= damageInterval)
            {
                // 스킬 레벨만큼 추가 데미지
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
