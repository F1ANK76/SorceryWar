using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillASpawner : MonoBehaviour
{
    public GameObject skillAPrefab; 
    public Transform player;        // 플레이어 위치
    public float spawnInterval = 1f; // 매초 1발, 초를 의미
    public float skillASpeed = 5f;

    public SkillManager skillManager; // SkillManager 참조
    private bool spawning = false;
    void Update()
    {
        // 0번째 스킬 레벨 체크, Lv1 이상이면 코루틴 시작
        if (!spawning && skillManager != null && skillManager.skills.Length > 0 && skillManager.skills[0].level >= 1)
        {
            StartCoroutine(SpawnSkillAs());
            spawning = true; // 중복 실행 방지
        }
    }

    IEnumerator SpawnSkillAs()
    {
        while (true)
        {
            SpawnSkillA();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnSkillA()
    {
        if (skillAPrefab == null || player == null) return;

        Vector3 spawnPos = player.position;

        var col = player.GetComponent<Collider2D>();

        if (col != null)
        {
            spawnPos.y = col.bounds.min.y; // 플레이어 Collider의 맨 아래
        }

        GameObject skillA = Instantiate(skillAPrefab, spawnPos, Quaternion.identity);

        var mover = skillA.AddComponent<SkillAMove>();
        mover.speed = skillASpeed;
        mover.skillManager = skillManager; // ← 여기서 할당

        AudioSource prefabAudio = skillA.GetComponent<AudioSource>();

        if (prefabAudio != null)
        {
            GameObject tempSound = new GameObject("TempSound");
            AudioSource audio = tempSound.AddComponent<AudioSource>();
            audio.clip = prefabAudio.clip;
            audio.volume = prefabAudio.volume;
            audio.pitch = prefabAudio.pitch;
            audio.spatialBlend = prefabAudio.spatialBlend;

            audio.Play();
            Destroy(tempSound, prefabAudio.clip.length); // 끝나면 삭제
        }
        else
        {
            Debug.Log("audio null");
        }
    }
}

public class SkillAMove : MonoBehaviour
{
    public float speed = 5f;
    public SkillManager skillManager; // 참조 추가
    public float baseDamage = 10f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                // 스킬 레벨만큼 추가 데미지
                int skillLevel = 0;
                if (skillManager != null && skillManager.skills.Length > 0)
                    skillLevel = skillManager.skills[0].level;

                float totalDamage = baseDamage + skillLevel;
                monster.TakeDamage(totalDamage);
            }

            Destroy(gameObject);
        }
    }
}

