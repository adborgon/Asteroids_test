using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroidPrefab;

    public Transform[] spawnList;
    public List<AsteroidHandler> asteroidsActive;

    public float rotationOffeset = 30;

    private void OnEnable()
    {
        StartCoroutine(AsteroidGenerator());
    }

    private IEnumerator AsteroidGenerator()
    {
        while (true)
        {
            if (asteroidsActive.Count < GameConfiguration.asteroidMaxNumber)
            {
                yield return new WaitForSeconds(GameConfiguration.asteroidCD);
                Transform spawnPointSelected = SpawnPoint();
                AsteroidHandler asteroid = Instantiate(asteroidPrefab, spawnPointSelected.position, Quaternion.identity).GetComponent<AsteroidHandler>();
                asteroidsActive.Add(asteroid);
                asteroid.InitAsteroid(this);
                asteroid.Shoot(Quaternion.Euler(0, 0, Random.Range(-rotationOffeset, rotationOffeset)) * spawnPointSelected.right);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private Transform SpawnPoint()
    {
        return spawnList[Random.Range(0, spawnList.Length)];
    }
}