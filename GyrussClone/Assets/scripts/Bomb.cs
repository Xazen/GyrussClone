using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Bomb : GameComponent<InGame>
{
    [SerializeField]
    private float speed = 30.0f;
    [SerializeField]
    private float maxLifeTime = 2.0f;

    public void Update()
    {
         // Move the projectile in a constant speed towards the center
         this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == InGame.Tag.SCREEN_FRAME ||
            col.gameObject.tag == InGame.Tag.PLAYER)
        {
            this.Game.objectPool.ReturnObject(this.gameObject, (int)InGame.ObjectPoolID.BombID);
        }
    }
}

