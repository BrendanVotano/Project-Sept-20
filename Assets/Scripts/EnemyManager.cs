using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ARCHER, ONEHAND, TWOHAND
}

public class EnemyManager : Singleton<EnemyManager>
{
    public int spawnCount = 5;
    public float spawnDelay = 0.3f;
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

    public void OnEnemyDied(Enemy _enemy)
    {
        enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);
    }

    void OnDifficultyChange(Difficulty _difficulty)
    {
        //if difficulty is easy, set spawn rate to 5
        if (_difficulty == Difficulty.EASY)
            spawnCount = 5;

        //if difficulty is medium, set spawn count to 10
        if (_difficulty == Difficulty.MEDIUM)
            spawnCount = 10;

        //if dfficulty is hard set spawn count to 15
        if (_difficulty == Difficulty.HARD)
            spawnCount = 15;
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDied += OnEnemyDied;
        GameEvents.OnDifficultyChange += OnDifficultyChange;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDied -= OnEnemyDied;
        GameEvents.OnDifficultyChange -= OnDifficultyChange;
    }
}
