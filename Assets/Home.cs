using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyDestination
{
    None,
    FrontDoor,
}

public class Home : MonoBehaviour
{
    [SerializeField]
    Transform _frontDoor = null;

    [SerializeField, MinValue(1)]
    int _totalHealth = 3;

    [ShowInInspector, ReadOnly]
    int _currentHealth = -1;

    [ShowInInspector]
    bool _jesusMode = false;

    Image[] _healthBar = null;

    void Awake()
    {
        RestoreToMaxHealth();

        _healthBar = GetComponentsInChildren(typeof(Image)).Cast<Image>().OrderBy(i => i.name).ToArray();
    }

    void Update()
    {
        for(var i = 0; i < _healthBar.Length; i++)
        {
            _healthBar[i].enabled = (_currentHealth - 1 >= i);
        }
    }

    public void RestoreToMaxHealth()
    {
        _currentHealth = _totalHealth;
    }

    public void DamageHome(int damage = 1)
    {
        _currentHealth = Math.Max(_jesusMode ? 1 : 0, _currentHealth - damage);
    }

    public bool HasRemainingHealth() { return _currentHealth > 0; }

    public Transform GetDestination(EnemyDestination destination)
    {
        switch(destination)
        {
            case EnemyDestination.FrontDoor:
            {
                return _frontDoor;
            }
            default:
            {
                throw new NotImplementedException($"Unhandled destination {destination}");
            }
        }
    }
}
