using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfiguration
{
    public static string gameName = "Asteroids Adrian";

    //Player

    public static float playerCD = 3f;

    //Bullet settings

    public static float bulletSpeed = 2000;
    public static float bulletLifeTime = 1.5f;
    public static float bulletCD = 0.2f;

    //asteroid settings

    public static int asteroidMaxNumber = 25;
    public static int asteroidSpeed = 300;
    public static float asteroidCD = 0.75f;
    public static int asteroidLifeTime = 8;
    public static float[] asteroidScaleRelationSize = new float[] { 0.3f, 0.7f, 1f };
    public static int[] asteroidHealthRelationSize = new int[] { 1, 2, 3 };
    public static float[] asteroidMassRelationSize = new float[] { 0.45f, 0.5f, 0.55f };
}