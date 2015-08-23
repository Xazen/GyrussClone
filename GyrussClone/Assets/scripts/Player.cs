using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Player : GameComponent<InGame>
{
    #region Projectile variables
    [SerializeField]
    private float fireRate = 1.0f;
    #endregion

    #region movement variables
    [SerializeField]
    private float rotationSpeed = 50.0f;

    private bool shouldRotateLeft = false;
    private bool shouldRotateRight = false;

    private Vector3? rotatePoint;
    #endregion

    public void Start ()
    {
        // Setup delegate methods
        this.Game.gameInput.KeyDown += KeyDown;
        this.Game.gameInput.KeyUp += KeyUp;

        // Get the rotation point from the spawn point position
        rotatePoint = GameObject.FindGameObjectWithTag(InGame.Tag.SPAWN_POINT).transform.position;
    }

    public void Update()
    {
        // Check whether the player should move
        if (shouldRotateLeft)
        {
            RotateLet();
        }

        if (shouldRotateRight)
        {
            RotateRight();
        }
    }

    #region Player Actions
    /// <summary>
    /// Player moves anticlockwise
    /// </summary>
    public void RotateLet()
    {
        if (rotatePoint.HasValue)
        {
            this.transform.RotateAround(rotatePoint.Value, Vector3.back, Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// Player moves clockwise
    /// </summary>
    public void RotateRight()
    {
        if (rotatePoint.HasValue)
        {
            this.transform.RotateAround(rotatePoint.Value, Vector3.forward, Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// Fires a projectile with the frequency specified by fireRate
    /// </summary>
    /// <returns></returns>
    public IEnumerator Fire()
    {
        while (true)
        {
            GameObject p = this.Game.objectPool.GetInstance((int) InGame.ObjectPoolID.ProjectileID);
            p.transform.position = this.transform.position;
            p.transform.rotation = this.transform.rotation;
            
            for (float timer = 0; timer <= fireRate; timer += Time.deltaTime)
            {
                yield return 0;
            }
        }
    }
    #endregion

    #region GameInputDelegate
    /// <summary>
    /// Called when a key is pressed
    /// </summary>
    /// <param name="keyMappingName"></param>
    public void KeyDown(string keyMappingName)
    {
        KeyCode keyCode = this.Game.gameInput.GetKeyMapping(keyMappingName).keyCode;

        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                shouldRotateRight = false;
                shouldRotateLeft = true;
                break;
            case KeyCode.RightArrow:
                shouldRotateRight = true;
                shouldRotateLeft = false;
                break;
            case KeyCode.Space:
                StartCoroutine("Fire");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Called when a key is released
    /// 
    /// </summary>
    /// <param name="keyMappingName"></param>
    public void KeyUp(string keyMappingName)
    {
        KeyCode keyCode = this.Game.gameInput.GetKeyMapping(keyMappingName).keyCode;

        switch (keyCode)
        {
            case KeyCode.LeftArrow:
                shouldRotateLeft = false;
                break;
            case KeyCode.RightArrow:
                shouldRotateRight = false;
                break;
            case KeyCode.Space:
                StopCoroutine("Fire");
                break;
            default:
                break;
        }
    }
    #endregion

    public void OnTriggerEnter(Collider col)
    {
        // Return this projectile to the object pool when it reaches the spawn point (center)
        if (col.gameObject.tag == InGame.Tag.BOMB)
        {
            // Reduce player health
            Image healthImage = GameObject.FindGameObjectWithTag(InGame.Tag.PLAYER_HEALTH).GetComponent<Image>();
            healthImage.fillAmount = healthImage.fillAmount - 0.2f;

            // Show game over screen
            if (healthImage.fillAmount <= 0)
            {
                this.Game.showGameOver();
            }
        }
    }
}

