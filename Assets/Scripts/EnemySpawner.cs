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
    Transform _destination = null;
    [SerializeField]
    LayerMask _mask = -1;

    void Awake()
    {
        InvokeRepeating("Spawn", _startAfterSeconds, _spawnRateSeconds);
    }

    void Spawn()
    {
        NavMeshAgent enemy = GameObject.Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(_destination.position, out hit, 10f, _mask))
        {
            enemy.SetDestination(hit.position);
        }
        else
        {
            Debug.LogError("Couldn't find nav mesh position!");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
