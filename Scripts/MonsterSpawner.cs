using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;   // �Ϲ� ���� ������
    public GameObject bossPrefab;      // ���� ������ �߰�

    public float spawnInterval = 3f;
    public float moveStep = 0.5f;

    public WaveManager waveManager;

    private int spawnCount = 0; // ��ȯ Ƚ�� ī��Ʈ

    void Start()
    {
        if (monsterPrefab == null) Debug.LogError("[Spawner] monsterPrefab ������");
        if (bossPrefab == null) Debug.LogError("[Spawner] bossPrefab ������");

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnMonster()
    {
        spawnCount++;

        GameObject prefabToSpawn;

        if (spawnCount % 10 == 0)
            prefabToSpawn = bossPrefab;
        else
            prefabToSpawn = monsterPrefab;

        var obj = Instantiate(prefabToSpawn, new Vector3(0, -5, 0), Quaternion.identity);
    }
}
