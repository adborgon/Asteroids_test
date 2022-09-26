using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfiguration
{
    public static string titleCanvas = "Asteroids";
    public static string subtitleCanvas = "Press R to start";
    public static string finishCanvas = "Game Over";
    public static string subFinishCanvas = "Score: ";
    public static string subFinishCanvasEnd = "\n Press R to restart";

    //Player

    public static float playerCD = 3f;
    public static float playerAccelSpeed = 12f;
    public static float playerTurnSpeed = 15f;
    public static float respawnInvulnerability = 3f;
    public static int playerMaxHealth = 3;
    public static int playerMaxEnery = 100;

    public static float shieldCD = 10f;
    public static float shieldLifeTime = 4f;
    public static int shieldEnergyCost = 20;

    //Machine Gun Player settings

    public static float machineGunSpeed = 400;
    public static float machineGunLifeTime = 0.85f;
    public static float machineGunCD = 0.16f;

    //asteroid settings

    public static int asteroidMaxNumber = 15;
    public static int asteroidSpeed = 150;
    public static float asteroidCD = 0.75f;
    public static int asteroidLifeTime = 15;
    public static int asteroidPointsRelatedWithSize = 100;
    public static float[] asteroidScaleRelationSize = new float[] { 0.3f, 0.7f, 1f };
    public static int[] asteroidHealthRelationSize = new int[] { 1, 2, 3 };
    public static float[] asteroidMassRelationSize = new float[] { 0.45f, 0.5f, 0.55f };

    //Enemy settings

    public static int enemyMaxNumber = 1;
    public static int enemySpeed = 30;
    public static float enemyCD = 15f;
    public static int enemyLifeTime = 50;
    public static int enemyPoints = 400;

    //Enemy Bomb settings

    public static float enemyBombSpeed = 15;
    public static float enemyBombLifeTime = 4.5f;
    public static float enemyBombCD = 10f;
}