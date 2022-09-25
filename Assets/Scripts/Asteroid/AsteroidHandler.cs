using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public Asteroid asteroid = new Asteroid();
    private AsteroidManager _asteroidManager;

    public void InitAsteroid(AsteroidManager asteroidManager)
    {
        _asteroidManager = asteroidManager;
        _rigidbody = GetComponent<Rigidbody2D>();
        asteroid.size = RandomeSize();
        asteroid.initConfiguration(_rigidbody, transform);
    }

    public void InitAsteroid(AsteroidManager asteroidManager, Asteroid.Size size)
    {
        if (size != Asteroid.Size.small)
        {
            _asteroidManager = asteroidManager;
            _rigidbody = GetComponent<Rigidbody2D>();
            asteroid.size = size - 1;
            asteroid.initConfiguration(_rigidbody, transform);
        }
    }

    public void Shoot(Vector2 direction)
    {
        _rigidbody.AddTorque(asteroid.turnDirection * asteroid.turnSpeed);
        _rigidbody.AddForce(direction * GameConfiguration.asteroidSpeed);
        Destroy(gameObject, GameConfiguration.asteroidLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                collision.SendMessage("Beaten");
                break;

            case "Shield":
                asteroid.health = 0;
                CheckAsteroidStatus();
                break;

            case "Bullet":
                asteroid.health--;
                CheckAsteroidStatus();
                break;

            default:
                break;
        }
    }

    private void CheckAsteroidStatus()
    {
        if (!_asteroidManager) return;
        if (asteroid.health <= 0)
        {
            if (asteroid.size != Asteroid.Size.small)
            {
                InstantiateAsteroidAfterDestruction();
                InstantiateAsteroidAfterDestruction();
            }
            PlayerManager.playerManager.UpdateScore(100 * ((int)asteroid.size + 1));
            Destroy(gameObject);
        }
    }

    private void InstantiateAsteroidAfterDestruction()
    {
        AsteroidHandler newAsteroid = Instantiate(_asteroidManager.asteroidPrefab, transform.position, Quaternion.identity).GetComponent<AsteroidHandler>();
        newAsteroid.InitAsteroid(_asteroidManager, asteroid.size);
        newAsteroid.Shoot(Quaternion.Euler(0, 0, Random.Range(0, 360)) * transform.right);
    }

    /// <summary>
    /// Asteroid type randomization
    /// </summary>
    /// <returns></returns>
    private Asteroid.Size RandomeSize()
    {
        //we could give different weights to the different sizes, e.g. more probability to the larger one
        return (Asteroid.Size)Random.Range(0, 3);
    }

    private void OnDestroy()
    {
        if (!_asteroidManager) return;
        _asteroidManager.asteroidsActive.Remove(this);
    }
}