using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int health;
    public int points;

    public Player()
    {
        this.health = 3;
    }
}