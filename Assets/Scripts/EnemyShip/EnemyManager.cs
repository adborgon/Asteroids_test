using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager { get; private set; }
    public GameObject enemyPrefab;
    public BoundarySetter[] spawnList;
    public List<EnemyHandler> enemiesActive;

    private float offset = 5;

    private void Awake()
    {
        enemyManager = this;
    }

    public void InitEnemyGenerator()
    {
        StartCoroutine(EnemyGenerator());
    }

    private IEnumerator EnemyGenerator()
    {
        while (true)
        {
            if (enemiesActive.Count < GameConfiguration.enemyMaxNumber)
            {
                yield return new WaitForSeconds(GameConfiguration.enemyCD);
                Vector3 pointSelected = SpawnPoint();
                EnemyHandler enemy = Instantiate(enemyPrefab, pointSelected, Quaternion.identity).GetComponent<EnemyHandler>();
                enemiesActive.Add(enemy);
                enemy.InitEnemy(this);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private Vector3 SpawnPoint()
    {
        BoundarySetter boundaryRelation = spawnList[Random.Range(0, spawnList.Length)];
        switch (boundaryRelation.boundaryPosition)
        {
            case BoundarySetter.Position.top:
                return boundaryRelation.transform.position + Vector3.down * offset;

            case BoundarySetter.Position.left:
                return boundaryRelation.transform.position + Vector3.right * offset;

            case BoundarySetter.Position.right:
                return boundaryRelation.transform.position + Vector3.left * offset;

            case BoundarySetter.Position.bottom:
                return boundaryRelation.transform.position + Vector3.up * offset;

            default:
                return Vector3.zero;
        }
    }
}