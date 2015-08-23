using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Enemy : GameComponent<InGame>
{
    [SerializeField]
    private float apporoachSpeed = 10.0f;

    [SerializeField]
    private float rotationSpeed = 50.0f;

    [SerializeField]
    private float minShootRate = 1.0f;
    private float maxShootRate = 3.0f;
    private Vector3? rotatePoint;

    private float minApproachTime = 0.2f;
    private float maxApporoachTime = 0.6f;

    public void Start()
    {
        // Get rotation point (center)
        rotatePoint = GameObject.FindGameObjectWithTag(InGame.Tag.SPAWN_POINT).transform.position;
    }

    public void OnEnable()
    {
        // Start moving towards screen frame
        StartCoroutine(Approaching());

        // Start shooting bombs
        StartCoroutine("ShootBomb");
    }

    public void Update()
    {
        if (rotatePoint.HasValue)
        {
            this.transform.RotateAround(rotatePoint.Value, Vector3.back, Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// Move towards screen frame
    /// </summary>
    /// <returns></returns>
    public IEnumerator Approaching()
    {
        float approachTime = UnityEngine.Random.Range(minApproachTime, maxApporoachTime);
        for (float timer = 0.0f; timer <= approachTime; timer += Time.deltaTime)
        {
            this.transform.Translate(Vector3.down * apporoachSpeed * Time.deltaTime);
            yield return 0;
        }
    }

    /// <summary>
    /// Shoot bombs
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootBomb()
    {
        while (true)
        {
            // Wait a while
            float shootRate = UnityEngine.Random.Range(minShootRate, maxShootRate);
            for (float timer = 0.0f; timer <= shootRate; timer += Time.deltaTime)
            {
                yield return 0;
            }

            // Fire a bomb
            GameObject bomb = this.Game.objectPool.GetInstance((int)InGame.ObjectPoolID.BombID);
            bomb.transform.position = this.transform.position;
            bomb.transform.rotation = this.transform.rotation;
        }
    }

    public void OnTriggerEnter (Collider col)
    {
        // Enemy got hit by a projectile
        if (col.gameObject.tag == InGame.Tag.PROJECTILE)
        {
            // Increase score
            this.Game.score += 100;
            Text score = GameObject.FindGameObjectWithTag(InGame.Tag.SCORE).GetComponent<Text>();
            score.text = this.Game.score.ToString();

            // Return enemy to object pool
            this.Game.objectPool.ReturnObject(this.gameObject, (int) InGame.ObjectPoolID.EnemeyID);
        }
    }
}

