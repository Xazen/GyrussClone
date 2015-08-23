using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Projectile : GameComponent<InGame>
{
    public static int ID = 1;

    [SerializeField]
    private float speed = 30.0f;

    public void Update()
    {
        // Move the projectile in a constant speed towards the center
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void OnTriggerEnter (Collider col)
    {
        // Return this projectile to the object pool when it reaches the spawn point (center)
        if (col.gameObject == GameObject.FindGameObjectWithTag(InGame.Tag.SPAWN_POINT))
        {
            this.Game.objectPool.ReturnObject(this.gameObject, Projectile.ID);
        }
    }
}

