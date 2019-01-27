using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _home = null;
    [SerializeField]
    float _goHomeStartSeconds = 1f;
    [SerializeField]
    float _goHomeRepeatSeconds = 3f;
    [SerializeField]
    float _randomDistance = 1f;
    [SerializeField]
    Sprite _horizontal = null;
    [SerializeField]
    Sprite _front = null;
    [SerializeField]
    Sprite _back = null;

    NavMeshAgent _agent = null;
    SpriteRenderer _spriteRenderer = null;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("GoHome", _goHomeStartSeconds, _goHomeRepeatSeconds);
    }

    void Update()
    {
        if(!_agent.hasPath || _agent.desiredVelocity.magnitude <= 0f)
        {
            _agent.SetDestination(RandomNavMesh());
        }
    }

    void LateUpdate()
    {
        bool showHorizontal = Mathf.Abs(_agent.desiredVelocity.x) > Mathf.Abs(_agent.desiredVelocity.z);
        if(showHorizontal)
        {
            _spriteRenderer.sprite = _horizontal;
            if(_agent.desiredVelocity.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            if(_agent.desiredVelocity.z > 0)
            {
                _spriteRenderer.sprite = _front;
            }
            else
            {
                _spriteRenderer.sprite = _back;
            }
        }
    }

    void GoHome()
    {
        _agent.SetDestination(_home.position);
    }

    Vector3 RandomNavMesh()
    {
        Vector3 random = _home.position;
        var randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        if(NavMesh.SamplePosition(transform.position + (randomDirection * _randomDistance), out var hit, 1f, -1))
        {
            random = hit.position;
        }
        return random;
    }
}
