using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

public enum LevelState
{
    None,
    Running,
    Complete
}

public class Level : MonoBehaviour
{
    [ShowInInspector]
    LevelState _state = LevelState.None;

    EnemySpawner[] _spawners = null;
    Enemy[] _remainingEnemies = null;

    public LevelState GetState() { return _state; }

    void Awake()
    {
        _spawners = FindObjectsOfType<EnemySpawner>();
        _state = LevelState.Running;
    }

    void Update()
    {
        if(_state == LevelState.Running)
        {
            if(_spawners.All(s => !s.HasSpawnsRemaining()))
            {
                if(_remainingEnemies == null)
                {
                    _remainingEnemies = FindObjectsOfType<Enemy>();
                }

                if(_remainingEnemies.All(r => r == null) || !_remainingEnemies.Any())
                {
                    _state = LevelState.Complete;
                }
            }
        }
    }
}
