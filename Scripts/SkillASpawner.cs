using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillASpawner : MonoBehaviour
{
    public GameObject skillAPrefab; 
    public Transform player;        // �÷��̾� ��ġ
    public float spawnInterval = 1f; // ���� 1��, �ʸ� �ǹ�
    public float skillASpeed = 5f;

    public SkillManager skillManager; // SkillManager ����
    private bool spawning = false;
    void Update()
    {
        // 0��° ��ų ���� üũ, Lv1 �̻��̸� �ڷ�ƾ ����
        if (!spawning && skillManager != null && skillManager.skills.Length > 0 && skillManager.skills[0].level >= 1)
        {
            StartCoroutine(SpawnSkillAs());
            spawning = true; // �ߺ� ���� ����
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
            spawnPos.y = col.bounds.min.y; // �÷��̾� Collider�� �� �Ʒ�
        }

        GameObject skillA = Instantiate(skillAPrefab, spawnPos, Quaternion.identity);

        var mover = skillA.AddComponent<SkillAMove>();
        mover.speed = skillASpeed;
        mover.skillManager = skillManager; // �� ���⼭ �Ҵ�

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
            Destroy(tempSound, prefabAudio.clip.length); // ������ ����
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
    public SkillManager skillManager; // ���� �߰�
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
                // ��ų ������ŭ �߰� ������
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

