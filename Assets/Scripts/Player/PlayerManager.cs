using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //singleton
    public static PlayerManager playerManager { get; private set; }

    public Player player;
    public GameObject playerShip;

    [SerializeField]
    private GameObject playerPrebaf;

    private bool _gameFinished = false;

    private void Awake()
    {
        playerManager = this;
        player = new Player();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameFinished)
        {
            GetComponent<GameManager>().ResetGame();
        }
    }

    public void UpdateScore(int pointsToIncrease)
    {
        player.score += pointsToIncrease;
        GUIHandler.guiHandler.UpdateScore(player.score);
    }

    public void UpdateEnergy(int energyToDecrease)
    {
        player.energy -= energyToDecrease;
        GUIHandler.guiHandler.UpdateEnergy(player.energy);
    }

    public void PlayerBeaten()
    {
        player.health--;
        GUIHandler.guiHandler.UpdateLife(player.health);
        if (player.health <= 0)
        {
            Debug.Log("Game Over");
            _gameFinished = true;
            GUIHandler.guiHandler.HideGUI();
            InfoHandler.infoHandler.ShowFinish(player.score);
        }
        else
        {
            Invoke("PlayerSpawn", GameConfiguration.playerCD);
        }
    }

    public void PlayerSpawn()
    {
        playerShip = Instantiate(playerPrebaf, Vector3.zero, Quaternion.identity);
    }
}