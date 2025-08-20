using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;   // 일반 몬스터 프리팹
    public GameObject bossPrefab;      // 보스 프리팹 추가

    public float spawnInterval = 3f;
    public float moveStep = 0.5f;

    public WaveManager waveManager;

    private int spawnCount = 0; // 소환 횟수 카운트

    void Start()
    {
        if (monsterPrefab == null) Debug.LogError("[Spawner] monsterPrefab 미지정");
        if (bossPrefab == null) Debug.LogError("[Spawner] bossPrefab 미지정");

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
