using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameStarted = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_gameStarted)
        {
            _gameStarted = true;
            InfoHandler.infoHandler.HideText();
            GUIHandler.guiHandler.ShowGUI();
            PlayerManager.playerManager.PlayerSpawn();
            AsteroidManager.asteroidManager.InitAsteroidGenerator();
            EnemyManager.enemyManager.InitEnemyGenerator();
        }
    }

    public void ResetGame()
    {
        /*PlayerManager.player.health = 3;
        PlayerManager.player.score = 0;
        InfoHandler.infoHandler.ShowIntro();
        GUIHandler.guiHandler.HideGUI();
        _gameStarted = false;*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}