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

    public void Start()
    {
        StartCoroutine(DestroyBomb());
    }

    public void Update()
    {
        // Move the projectile in a constant speed towards the center
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit something");
        // Return this projectile to the object pool when it reaches the spawn point (center)
        if (col.gameObject == GameObject.FindGameObjectWithTag(InGame.Tag.PLAYER))
        {
            Debug.Log("Hit player");
            // Reduce player health
            Image healthImage = GameObject.FindGameObjectWithTag(InGame.Tag.PLAYER_HEALTH).GetComponent<Image>();
            healthImage.fillAmount = healthImage.fillAmount - 0.2f;

            // Show game over screen
            if (healthImage.fillAmount <= 0)
            {
                this.Game.showGameOver();
            }

            this.Game.objectPool.ReturnObject(this.gameObject, (int) InGame.ObjectPoolID.BombID);
        }
    }

    /// <summary>
    /// Destroy the bomb after a the life time ends
    /// </summary>
    /// <returns></returns>
    public IEnumerator DestroyBomb()
    {
        for (float timer = 0.0f; timer <= maxLifeTime; timer += Time.deltaTime)
        {            
            yield return 0;
        }

        Debug.Log("returned");
        this.Game.objectPool.ReturnObject(this.gameObject, (int)InGame.ObjectPoolID.BombID);
    }
}

