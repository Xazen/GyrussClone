using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class Player : GameComponent<InGame>
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private int projectilePool = 20;
    [SerializeField]
    private float fireRate = 1.0f;
    private int currentProjectiles = 0;

    [SerializeField]
    private float rotationSpeed = 50.0f;

    private bool shouldRotateLeft = false;
    private bool shouldRotateRight = false;
    private bool shouldFire = false;

    private Vector3? rotatePoint;

    public void Start ()
    {
        // Prepare projectiles
        this.Game.objectPool.CreateNewObjectPoolEntry(projectile, Projectile.ID, projectilePool);

        // Setup delegate methods
        this.Game.gameInput.KeyDown += KeyDown;
        this.Game.gameInput.KeyUp += KeyUp;

        rotatePoint = GameObject.FindGameObjectWithTag(InGame.Tag.SPAWN_POINT).transform.position;
    }

    public void Update()
    {
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
    public void RotateLet()
    {
        if (rotatePoint.HasValue)
        {
            this.transform.RotateAround(rotatePoint.Value, Vector3.back, Time.deltaTime * rotationSpeed);
        }
    }

    public void RotateRight()
    {
        if (rotatePoint.HasValue)
        {
            this.transform.RotateAround(rotatePoint.Value, Vector3.forward, Time.deltaTime * rotationSpeed);
        }
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            GameObject p = this.Game.objectPool.GetInstance(Projectile.ID);
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
}

