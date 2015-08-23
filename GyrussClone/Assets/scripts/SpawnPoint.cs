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
    private float minStartSpawnTime = 5.0f;
    [SerializeField]
    private float maxStartSpawnTime = 7.5f;
    [SerializeField]
    private float spawnMultiplier = 0.9f;

    public void Start()
    {
        StartCoroutine(StartSpawning());
    }

    public IEnumerator StartSpawning()
    {
        while (true)
        {
            // Spawn enemies
            StartCoroutine(SpawnEnemies());

            // Wait before spawning next swarm
            float waitTime = UnityEngine.Random.Range(minStartSpawnTime, maxStartSpawnTime);
            for (float timer = 0.0f; timer <= waitTime; timer += Time.deltaTime)
            {
                yield return 0;
            }

            // Reduce spawn time
            minStartSpawnTime *= spawnMultiplier;
            maxStartSpawnTime *= spawnMultiplier;
        }
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < swarmSize; i++ )
        {
            for (float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime)
            {
                yield return 0;
            }

            GameObject enemy = this.Game.objectPool.GetInstance((int)InGame.ObjectPoolID.EnemeyID);
            enemy.transform.position = this.transform.position;
        }
    }
}