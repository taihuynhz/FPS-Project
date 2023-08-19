using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    protected static EnemySpawner instance;
    public static EnemySpawner Instance => instance;

    [SerializeField] public int enemyAmount;
    [SerializeField] public Transform holder;

    protected void Awake() => CreateSingleton();

    protected void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (spawnCount >= enemyAmount) return;
        GameObject enemy = Spawn(holder);

        if (enemy == null) return;
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    public void SpawnEnemy(float position)
    {
        if (spawnCount >= enemyAmount) return;
        GameObject enemy = Spawn();

        if (enemy == null) return;
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}