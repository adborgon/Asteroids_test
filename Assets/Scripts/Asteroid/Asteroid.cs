using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Asteroid
{
    public enum Size
    {
        small, medium, big
    }

    public Size size;

    public int health;
    public float turnSpeed = 10f;
    public float turnDirection = 0;

    public void InitConfiguration(Rigidbody2D rigidbody2D, Transform transform)
    {
        health = GameConfiguration.asteroidHealthRelationSize[(int)size];
        transform.localScale = Vector3.one * GameConfiguration.asteroidScaleRelationSize[(int)size];
        rigidbody2D.mass = GameConfiguration.asteroidMassRelationSize[(int)size];

        turnSpeed = UnityEngine.Random.Range(5f, 10f);
        //If they do not rotate it looks weird
        if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
            turnDirection = -1;
        else
            turnDirection = 1;
    }
}