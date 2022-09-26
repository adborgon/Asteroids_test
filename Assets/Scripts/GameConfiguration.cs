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
    public static float shieldCD = 5f;
    public static float shieldLifeTime = 200f;
    public static int shieldEnergyCost = 20;

    //Machine Gun settings

    public static float machineGunSpeed = 500;
    public static float machineGunLifeTime = 1f;
    public static float machineGunCD = 0.16f;

    //asteroid settings

    public static int asteroidMaxNumber = 25;
    public static int asteroidSpeed = 300;
    public static float asteroidCD = 0.75f;
    public static int asteroidLifeTime = 8;
    public static float[] asteroidScaleRelationSize = new float[] { 0.3f, 0.7f, 1f };
    public static int[] asteroidHealthRelationSize = new int[] { 1, 2, 3 };
    public static float[] asteroidMassRelationSize = new float[] { 0.45f, 0.5f, 0.55f };

    //Enemy settings

    public static int enemyMaxNumber = 1;
    public static int enemySpeed = 30;
    public static float enemyCD = 5f;
    public static int enemyLifeTime = 30;

    //Enemy Bomb settings

    public static float enemyBombSpeed = 15;
    public static float enemyBombLifeTime = 4.5f;
    public static float enemyBombCD = 5f;
}