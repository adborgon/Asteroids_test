using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Shoot();
    }

    private void Shoot()
    {
        _rigidbody.AddForce(direction * GameConfiguration.bulletSpeed);
        Destroy(gameObject, GameConfiguration.bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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