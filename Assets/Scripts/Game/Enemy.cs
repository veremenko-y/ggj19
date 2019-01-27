using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField, MinValue(1)]
    int _homeDamage = 1;
    [SerializeField]
    Sprite _horizontal = null;
    [SerializeField]
    Sprite _front = null;
    [SerializeField]
    Sprite _back = null;

    NavMeshAgent _agent = null;
    SpriteRenderer _spriteRenderer = null;

    public int GetHomeDamage() { return _homeDamage; }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
}
