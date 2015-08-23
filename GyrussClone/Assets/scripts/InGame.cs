using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class InGame : Game
{
    #region IDs
    /// <summary>
    /// IDs for object pooling
    /// </summary>
    public enum ObjectPoolID
    {
        ProjectileID = 0,
        EnemeyID = 1,
        BombID = 2
    }

    /// <summary>
    /// Tags to find GameObjects
    /// </summary>
    public class Tag
    {
        public static string PLAYER = "Player";
        public static string ENEMY = "Enemy";
        public static string PROJECTILE = "Projectile";
        public static string SPAWN_POINT = "SpawnPoint";
        public static string BOMB = "Bomb";
        public static string PLAYER_HEALTH = "PlayerHealth";
    }
    #endregion

    #region Object pool variables
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private int projectilePoolCount = 20;

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private int enemyPoolCount = 20;

    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private int bombPoolCount = 20;
    #endregion

    protected override void Initialize()
    {
        // Prepare projectiles, boms and enemies with the object pool
    }

    protected override void GameSetupReady()
    {
        this.objectPool.CreateNewObjectPoolEntry(enemy, (int)InGame.ObjectPoolID.EnemeyID, enemyPoolCount);
        this.objectPool.CreateNewObjectPoolEntry(projectile, (int)InGame.ObjectPoolID.ProjectileID, projectilePoolCount);
        this.objectPool.CreateNewObjectPoolEntry(bomb, (int)InGame.ObjectPoolID.BombID, bombPoolCount);
    }
}

