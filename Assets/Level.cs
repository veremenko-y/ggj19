using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

public enum LevelState
{
    None,
    Running,
    Complete,
    Failure
}

public class Level : MonoBehaviour
{
    [SerializeField]
    string _title = "Level";

    [SerializeField]
    int _waitBeforeStartSeconds = 2;

    [SerializeField]
    int _waitAfterEndSeconds = 2;

    [ShowInInspector, ReadOnly]
    LevelState _state = LevelState.None;

    EnemySpawner[] _spawners = null;
    Enemy[] _remainingEnemies = null;
    Home _home = null;

    public LevelState GetState() { return _state; }

    void Awake()
    {
        _home = FindObjectOfType<Home>();
        _spawners = FindObjectsOfType<EnemySpawner>();
        _state = LevelState.Running;

        _home.RestoreToMaxHealth();
    }

    void Update()
    {
        if(_state == LevelState.Running)
        {
            if(!_home.HasRemainingHealth())
            {
                _state = LevelState.Failure;
            }
            else if(_spawners.All(s => !s.HasSpawnsRemaining()))
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

    [Button()]
    void DebugCompleteLevel()
    {
        _state = LevelState.Complete;
    }
}
