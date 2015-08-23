using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class SpawnPoint : GameComponent<InGame>
{
    [SerializeField]
    private float swarmSize = 3;
    [SerializeField]
    private float spawnRate = 1.3f;

    [SerializeField]
    private float minStartSpawnTime = 5.0f;
    [SerializeField]
    private float maxStartSpawnTime = 7.5f;
    [SerializeField]
    private float spawnMultiplier = 0.9f;

    public void Start()
    {
        StartCoroutine(StartSpawning());
    }

    /// <summary>
    /// Start spawning enemies
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartSpawning()
    {
        while (true)
        {
            // Spawn enemies
            StartCoroutine(SpawnEnemySwarm());

            // Wait before spawning next swarm
            float waitTime = UnityEngine.Random.Range(minStartSpawnTime, maxStartSpawnTime);
            for (float timer = 0.0f; timer <= waitTime; timer += Time.deltaTime)
            {
                yield return 0;
            }

            // Reduce spawn time
            if (minStartSpawnTime >= 3.0f)
            {
                minStartSpawnTime *= spawnMultiplier;
            }

            if (maxStartSpawnTime >= 3.0f)
            {
                maxStartSpawnTime *= spawnMultiplier;
            }
        }
    }

    /// <summary>
    /// Spawn enemies
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnEnemySwarm()
    {
        int startPoint = UnityEngine.Random.Range(0, 360);

        for (int i = 0; i < swarmSize; i++ )
        {
            for (float timer = 0.0f; timer < spawnRate; timer += Time.deltaTime)
            {
                yield return 0;
            }

            GameObject enemy = this.Game.objectPool.GetInstance((int)InGame.ObjectPoolID.EnemeyID);
            enemy.transform.position = this.transform.position;
            enemy.transform.rotation = Quaternion.Euler(0, 0, startPoint);
        }
    }
}