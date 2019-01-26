using Sirenix.OdinInspector;
using UnityEngine;

public class Sprinkler : MonoBehaviour
{
    [SerializeField]
    float _triggerCooldownSeconds = 5f;
    [ShowInInspector, ReadOnly]
    float _remainingCooldownSeconds = 0f;

    Animator _animator = null;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Trigger();
    }

    [Button("Debug Trigger")]
    void Trigger()
    {
        if(_remainingCooldownSeconds > 0f)
        {
            _animator.SetBool("Activate", true);
            _remainingCooldownSeconds = _triggerCooldownSeconds;
        }
    }

    void Update()
    {
        if(_remainingCooldownSeconds > 0f)
        {
            _remainingCooldownSeconds -= Time.deltaTime;
        }
    }
}
