using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int spawnCount = 5;
    public float spawnDelay = 1;
    int enemyCount = 0;
    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWithDelay(spawnDelay));
    }

    IEnumerator SpawnWithDelay(float _delay)
    {
        while (enemyCount < spawnCount)
        {
            int rnd = Random.Range(0, enemyPrefabs.Length);
            Vector3 newPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            GameObject go = Instantiate(enemyPrefabs[rnd], newPos, transform.rotation);
            enemyCount++;
            yield return new WaitForSeconds(_delay);
        }
    }
}
