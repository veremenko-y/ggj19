using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Trap : MonoBehaviour
{
    public AudioClip PlaceSound;
    public AudioClip TriggerSound;

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

    private void Start()
    {
        _audioSource.clip = PlaceSound;
        _audioSource.Play();
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
            _audioSource.clip = TriggerSound;
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
