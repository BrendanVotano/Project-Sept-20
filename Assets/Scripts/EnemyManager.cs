using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public int spawnCount = 5;
    public float spawnDelay = 1;
    int enemyCount = 0;
    public GameObject[] enemyPrefabs;

    public List<Enemy> enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWithDelay(spawnDelay));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            enemies[enemies.Count-1].speed += 1;

        if (Input.GetKeyDown(KeyCode.O))
            enemies[enemies.Count-1].speed -= 1;

    }

    IEnumerator SpawnWithDelay(float _delay)
    {
        while (enemyCount < spawnCount)
        {
            int rnd = Random.Range(0, enemyPrefabs.Length);
            Vector3 newPos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            GameObject go = Instantiate(enemyPrefabs[rnd], newPos, transform.rotation);
            enemies.Add(go.GetComponent<Enemy>());
            enemyCount++;
            yield return new WaitForSeconds(_delay);
        }
    }

    public void EnemyDied(Enemy _enemy)
    {
        enemies.Remove(_enemy);
    }
}
