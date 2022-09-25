using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    //singleton
    public PlayerConfig Config { get; private set; }

    private void Awake()
    {
        Config = this;
    }
}