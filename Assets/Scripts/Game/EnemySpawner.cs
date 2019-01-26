using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent _enemyPrefab = null;
    [SerializeField]
    float _startAfterSeconds = 10f;
    [SerializeField]
    float _spawnRateSeconds = 10f;
    [SerializeField]
    int _numberToSpawn = 10;
    [SerializeField]
    EnemyDestination _destination = EnemyDestination.None;
    [SerializeField]
    LayerMask _mask = -1;

    [ShowInInspector, ReadOnly]
    int _numberSpawned = 0;

    Home _home = null;

    void Awake()
    {
        _home = FindObjectOfType<Home>();
        InvokeRepeating("Spawn", _startAfterSeconds, _spawnRateSeconds);
    }

    void Spawn()
    {
        NavMeshAgent enemy = Instantiate(_enemyPrefab, transform);
        Transform destination = _home.GetDestination(_destination);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(destination.position, out hit, 10f, _mask))
        {
            enemy.SetDestination(hit.position);
        }
        else
        {
            Debug.LogError("Couldn't find nav mesh position!");
        }

        _numberSpawned++;
        if(_numberSpawned >= _numberToSpawn)
        {
            CancelInvoke("Spawn");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
