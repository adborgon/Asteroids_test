using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Enemy asteroid = new Enemy();
    public GameObject bulletPrefab;
    public GameObject exclamation;
    private EnemyManager _enemyManager;
    private float _accelerationTime = 2f;
    private float _timeLeftToAccelerate;

    public void InitEnemy(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());
        StartCoroutine(HideExclamation());
        Destroy(gameObject, GameConfiguration.enemyLifeTime);
    }

    private void Update()
    {
        _timeLeftToAccelerate -= Time.deltaTime;
        if (_timeLeftToAccelerate <= 0)
        {
            _timeLeftToAccelerate = _accelerationTime;
            _rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * GameConfiguration.enemySpeed);
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameConfiguration.enemyBombCD);
            if (PlayerManager.playerManager.player)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Bullet>().direction = PlayerManager.playerManager.player.transform.position - transform.position; //Vector between player and enemy
            }
        }
    }

    private IEnumerator HideExclamation()
    {
        yield return new WaitForSeconds(2);
        exclamation.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                collision.SendMessage("Beaten");
                break;

            case "Shield":
                PlayerManager.playerManager.UpdateScore(GameConfiguration.enemyPoints);
                Destroy(gameObject);
                break;

            case "Bullet":
                if (collision.gameObject.GetComponent<Bullet>().bulletChoose == Bullet.BulletType.enemy) return;
                PlayerManager.playerManager.UpdateScore(GameConfiguration.enemyPoints);
                Destroy(gameObject);
                break;

            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (!_enemyManager) return;
        _enemyManager.enemiesActive.Remove(this);
    }
}