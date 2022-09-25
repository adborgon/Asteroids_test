using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //singleton
    public static PlayerManager playerManager { get; private set; }

    public int health = 3;
    public int score;
    public int energy = 100;

    public GameObject player;

    [SerializeField]
    private GameObject playerPrebaf;

    public bool gameFinished = false;

    private void Awake()
    {
        playerManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameFinished)
        {
            GetComponent<GameManager>().ResetGame();
        }
    }

    public void UpdateScore(int pointsToIncrease)
    {
        score += pointsToIncrease;
        GUIHandler.guiHandler.UpdateScore(score);
    }

    public void UpdateEnergy(int energyToDecrease)
    {
        energy -= energyToDecrease;
        GUIHandler.guiHandler.UpdateEnergy(energy);
    }

    public void PlayerBeaten()
    {
        health--;
        GUIHandler.guiHandler.UpdateLife(health);
        if (health <= 0)
        {
            Debug.Log("Game Over");
            gameFinished = true;
            GUIHandler.guiHandler.HideGUI();
            InfoHandler.infoHandler.ShowFinish(score);
        }
        else
        {
            Invoke("PlayerSpawn", GameConfiguration.playerCD);
        }
    }

    public void PlayerSpawn()
    {
        player = Instantiate(playerPrebaf, Vector3.zero, Quaternion.identity);
    }
}