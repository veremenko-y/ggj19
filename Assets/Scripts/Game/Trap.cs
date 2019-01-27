using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Trap : MonoBehaviour
{
    public int Damage = 1;

    [SerializeField]
    readonly AudioClip _placeSound;
    [SerializeField]
    readonly AudioClip _triggerSound;
    [SerializeField]
    int _trapHealth = 5;
    [SerializeField]
    readonly float _triggerCooldownSeconds = 5f;
    [ShowInInspector, ReadOnly]
    float _remainingCooldownSeconds = 0f;

    Animator _animator = null;
    AudioSource _audioSource = null;
    SpriteRenderer _spriteRenderer = null;
    Collider _collider = null;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _audioSource.clip = _placeSound;
        _audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        TryTrigger();
    }

    [Button("Debug Trigger")]
    void TryTrigger()
    {
        if (_remainingCooldownSeconds <= 0f)
        {
            _audioSource.clip = _triggerSound;
            _audioSource.Play();
            _remainingCooldownSeconds = _triggerCooldownSeconds;
            _collider.enabled = false;
            _trapHealth--;
            SetColor(1, .5f, 0);
            if(_trapHealth <= 1)
            {
                StartCoroutine(Blink());
            }
            if(_trapHealth == 0)
            {
                Destroy(gameObject);
            }
            _animator.SetTrigger("Activate");
        }
    }

    void Update()
    {
        if (_remainingCooldownSeconds > 0f)
        {
            _remainingCooldownSeconds -= Time.deltaTime;
        }
        else
        {
            _collider.enabled = true;
            SetColor(1, 1, 1);
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            var isClear = _spriteRenderer.color.a == 0;
            SetAlpha(isClear ? 1 : 0);
            yield return new WaitForSeconds(.1f);
        }
    }

    void SetColor(float r, float g, float b)
    {
        var c = _spriteRenderer.color;
        c.r = r;
        c.g = g;
        c.b = b;
        _spriteRenderer.color = c;
    }

    void SetAlpha(float a)
    {
        var c = _spriteRenderer.color;
        c.a = a;
        _spriteRenderer.color = c;
    }
}
