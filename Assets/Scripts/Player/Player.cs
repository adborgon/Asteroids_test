using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int health = GameConfiguration.playerMaxHealth;
    public int score;
    public int energy = GameConfiguration.playerMaxEnery;
}