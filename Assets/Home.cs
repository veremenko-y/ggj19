using Sirenix.OdinInspector;
using System;
using UnityEngine;

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

    void Awake()
    {
        RestoreToMaxHealth();
    }

    public void RestoreToMaxHealth()
    {
        _currentHealth = _totalHealth;
    }

    public void DamageHome(int damage = 1)
    {
        _currentHealth = Math.Max(0, _currentHealth - damage);
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
