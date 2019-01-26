using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField, MinValue(1)]
    int _homeDamage = 1;

    Animator _animator = null;
    NavMeshAgent _agent = null;

    public int GetHomeDamage() { return _homeDamage; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // pass in desired direction to animator so we can determine what the sprite looks like
        _animator.SetFloat("x", _agent.desiredVelocity.x);
        // map z to y since depth looks like moving up or down
        _animator.SetFloat("y", _agent.desiredVelocity.z);
    }
}
