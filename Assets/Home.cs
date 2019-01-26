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
