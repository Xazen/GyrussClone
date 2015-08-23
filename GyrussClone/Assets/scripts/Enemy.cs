using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Enemy : GameComponent<InGame>
{
    [SerializeField]
    private float apporoachTime = 1.0f;

    [SerializeField]
    private float apporoachSpeed = 10.0f;

    [SerializeField]
    private float rotationSpeed = 50.0f;

    [SerializeField]
    private float shootRate = 2.0f;
    private Vector3? rotatePoint;

    public void Start()
    {
        // Get rotation point (center)
        rotatePoint = GameObject.FindGameObjectWithTag(InGame.Tag.SPAWN_POINT).transform.position;

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
        for (float timer = 0.0f; timer <= apporoachTime; timer += Time.deltaTime)
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
            for (float timer = 0.0f; timer <= shootRate; timer += Time.deltaTime)
            {
                yield return 0;
            }
            GameObject bomb = this.Game.objectPool.GetInstance((int)InGame.ObjectPoolID.BombID);
            bomb.transform.position = this.transform.position;
            bomb.transform.rotation = this.transform.rotation;
        }
    }

    public void OnTriggerEnter (Collider col)
    {
        if (col.gameObject == GameObject.FindGameObjectWithTag(InGame.Tag.PROJECTILE))
        {
            this.Game.score += 100;
            Text score = GameObject.FindGameObjectWithTag(InGame.Tag.SCORE).GetComponent<Text>();
            score.text = this.Game.score.ToString();

            this.Game.objectPool.ReturnObject(this.gameObject, (int) InGame.ObjectPoolID.EnemeyID);
        }
    }
}

