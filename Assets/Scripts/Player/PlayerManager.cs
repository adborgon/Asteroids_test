using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //singleton
    public static PlayerManager player { get; private set; }

    public int health = 3;
    public int points;

    [SerializeField]
    private GameObject playerPrebaf;

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        PlayerSpawn();
    }

    public void PlayerBeaten()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Invoke("PlayerSpawn", GameConfiguration.playerCD);
        }
    }

    public void PlayerSpawn()
    {
        Instantiate(playerPrebaf, Vector3.zero, Quaternion.identity);
    }
}