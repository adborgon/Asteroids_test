using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody;

    public enum BulletType
    {
        machineGun, enemy
    }

    public BulletType bulletChoose;

    private float bulletSpeed, bulletLifeTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        switch (bulletChoose)
        {
            case BulletType.machineGun:
                bulletSpeed = GameConfiguration.machineGunSpeed;
                bulletLifeTime = GameConfiguration.machineGunLifeTime;
                break;

            case BulletType.enemy:
                bulletSpeed = GameConfiguration.enemyBombSpeed;
                bulletLifeTime = GameConfiguration.enemyBombLifeTime;
                break;

            default:
                bulletSpeed = GameConfiguration.machineGunSpeed;
                bulletLifeTime = GameConfiguration.machineGunLifeTime;
                break;
        }
        Shoot();
    }

    public void Shoot()
    {
        _rigidbody.AddForce(direction * bulletSpeed);
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletChoose == BulletType.enemy)
        {
            switch (collision.tag)
            {
                case "Asteroid":
                case "Player":
                case "Shield":
                    Destroy(gameObject);
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (collision.tag)
            {
                case "Asteroid":
                    Destroy(gameObject);
                    break;

                default:
                    break;
            }
        }
    }
}