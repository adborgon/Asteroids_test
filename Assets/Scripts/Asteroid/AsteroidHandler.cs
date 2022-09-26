using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private AsteroidManager _asteroidManager;
    [SerializeField] private Asteroid asteroid = new Asteroid();

    #region Init Asteroid

    //Init instead Start due to instantiate problems, 1º init to AsteroidManager generator with random size, 2º to after destruction with specific size
    public void InitAsteroid(AsteroidManager asteroidManager)
    {
        _asteroidManager = asteroidManager;
        _rigidbody = GetComponent<Rigidbody2D>();
        asteroid.size = RandomeSize();
        asteroid.InitConfiguration(_rigidbody, transform);
    }

    public void InitAsteroid(AsteroidManager asteroidManager, Asteroid.Size size)
    {
        if (size != Asteroid.Size.small)
        {
            _asteroidManager = asteroidManager;
            _rigidbody = GetComponent<Rigidbody2D>();
            asteroid.size = size - 1;
            asteroid.InitConfiguration(_rigidbody, transform);
        }
    }

    #endregion Init Asteroid

    public void Launch(Vector2 direction)
    {
        _rigidbody.AddTorque(asteroid.turnDirection * asteroid.turnSpeed);
        _rigidbody.AddForce(direction * GameConfiguration.asteroidSpeed);
        Destroy(gameObject, GameConfiguration.asteroidLifeTime);
    }

    private void CheckHealthAndGivePoints(bool givePoints)
    {
        if (!_asteroidManager) return;
        if (asteroid.health <= 0)
        {
            if (asteroid.size != Asteroid.Size.small)
            {
                InstantiateAsteroidAfterDestruction();
                InstantiateAsteroidAfterDestruction();
            }
            if (givePoints) PlayerManager.playerManager.UpdateScore(GameConfiguration.asteroidPointsRelatedWithSize * ((int)asteroid.size + 1));
            Destroy(gameObject);
        }
    }

    private void InstantiateAsteroidAfterDestruction()
    {
        AsteroidHandler newAsteroid = Instantiate(_asteroidManager.asteroidPrefab, transform.position, Quaternion.identity).GetComponent<AsteroidHandler>();
        newAsteroid.InitAsteroid(_asteroidManager, asteroid.size);
        newAsteroid.Launch(Quaternion.Euler(0, 0, Random.Range(0, 360)) * transform.right);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                collision.SendMessage("Beaten");
                break;

            case "Shield":
                asteroid.health = 0;
                CheckHealthAndGivePoints(givePoints: true);
                break;

            case "Bullet":
                asteroid.health--;
                if (collision.gameObject.GetComponent<Bullet>().bulletChoose == Bullet.BulletType.enemy)
                    CheckHealthAndGivePoints(givePoints: false);
                else
                    CheckHealthAndGivePoints(givePoints: true);

                break;

            default:
                break;
        }
    }

    private void OnDestroy()
    {
        if (!_asteroidManager) return;
        _asteroidManager.RemoveAsteroidFromList(this);
    }
}