using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    //Singleton
    public static AsteroidManager asteroidManager { get; private set; }

    public GameObject asteroidPrefab;
    public Transform[] spawnList;
    private List<AsteroidHandler> _asteroidsActive = new List<AsteroidHandler>();
    private readonly float _rotationOffset = 45;

    private void Awake()
    {
        asteroidManager = this;
    }

    public void InitAsteroidGenerator()
    {
        StartCoroutine(AsteroidGenerator());
    }

    private IEnumerator AsteroidGenerator()
    {
        while (true)
        {
            if (_asteroidsActive.Count < GameConfiguration.asteroidMaxNumber)
            {
                yield return new WaitForSeconds(GameConfiguration.asteroidCD);
                Transform spawnPointSelected = GetSpawnPoint();
                AsteroidHandler asteroid = Instantiate(asteroidPrefab, spawnPointSelected.position, Quaternion.identity).GetComponent<AsteroidHandler>();
                _asteroidsActive.Add(asteroid);
                asteroid.InitAsteroid(this);
                asteroid.Launch(Quaternion.Euler(0, 0, Random.Range(-_rotationOffset, _rotationOffset)) * spawnPointSelected.right);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private Transform GetSpawnPoint()
    {
        return spawnList[Random.Range(0, spawnList.Length)];
    }

    public void RemoveAsteroidFromList(AsteroidHandler asteroidHandler)
    {
        _asteroidsActive.Remove(asteroidHandler);
    }
}