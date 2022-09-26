using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager { get; private set; }

    [SerializeField] private GameObject _enemyPrefab;
    public BoundarySetter[] spawnList;
    private List<EnemyHandler> _enemiesActive = new List<EnemyHandler>();
    private readonly float _spaenOffset = 5;

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
            if (_enemiesActive.Count < GameConfiguration.enemyMaxNumber)
            {
                yield return new WaitForSeconds(GameConfiguration.enemyCD);
                Vector3 pointSelected = GetSpawnPoint();
                EnemyHandler enemy = Instantiate(_enemyPrefab, pointSelected, Quaternion.identity).GetComponent<EnemyHandler>();
                _enemiesActive.Add(enemy);
                enemy.InitEnemy(this);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private Vector3 GetSpawnPoint()
    {
        BoundarySetter boundaryRelation = spawnList[Random.Range(0, spawnList.Length)];
        switch (boundaryRelation.boundaryPosition)
        {
            case BoundarySetter.Position.top:
                return boundaryRelation.transform.position + Vector3.down * _spaenOffset;

            case BoundarySetter.Position.left:
                return boundaryRelation.transform.position + Vector3.right * _spaenOffset;

            case BoundarySetter.Position.right:
                return boundaryRelation.transform.position + Vector3.left * _spaenOffset;

            case BoundarySetter.Position.bottom:
                return boundaryRelation.transform.position + Vector3.up * _spaenOffset;

            default:
                return Vector3.zero;
        }
    }

    public void RemoveEnemyFromList(EnemyHandler enemy)
    {
        _enemiesActive.Remove(enemy);
    }
}