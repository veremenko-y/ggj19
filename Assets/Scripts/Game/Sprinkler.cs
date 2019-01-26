using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sprinkler : MonoBehaviour
{
    [SerializeField]
    float _triggerCooldownSeconds = 5f;
    [ShowInInspector, ReadOnly]
    float _remainingCooldownSeconds = 0f;

    Animator _animator = null;
    AudioSource _audioSource = null;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        TryTrigger();
    }

    [Button("Debug Trigger")]
    void TryTrigger()
    {
        if(_remainingCooldownSeconds <= 0f)
        {
            _audioSource.Play();
            _remainingCooldownSeconds = _triggerCooldownSeconds;
            _animator?.SetTrigger("Activate");
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
