using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityGameBase;

public class InGame : Game
{
    public class Tag
    {
        public static string PLAYER = "Player";
        public static string ENEMY = "Enemy";
        public static string PROJECTILE = "Projectile";
        public static string SPAWN_POINT = "SpawnPoint";
        public static string BOMB = "Bomb";
        public static string PLAYER_HEALTH = "PlayerHealth";
    }

    protected override void Initialize()
    {

    }

    protected override void GameSetupReady()
    {

    }
}

