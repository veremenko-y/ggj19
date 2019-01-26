using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum LevelState
{
    None,
    Starting,
    Running,
    Complete,
    Failure
}

public class Level : MonoBehaviour
{
    [SerializeField]
    int _waitBeforeStartSeconds = 2;

    [SerializeField]
    int _waitAfterEndSeconds = 2;

    [SerializeField]
    Text _titleText = null;

    [ShowInInspector, ReadOnly]
    LevelState _state = LevelState.None;

    EnemySpawner[] _spawners = null;
    Enemy[] _remainingEnemies = null;
    Home _home = null;
    Coroutine _startingSequence = null;
    Coroutine _endingSequence = null;

    public LevelState GetState() { return _state; }

    void Awake()
    {
        _home = FindObjectOfType<Home>();
        _spawners = FindObjectsOfType<EnemySpawner>();
        _state = LevelState.Starting;

        _home.RestoreToMaxHealth();
    }

    IEnumerator StartingSequence()
    {
        _titleText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(_waitBeforeStartSeconds);

        _titleText.gameObject.SetActive(false);
        _state = LevelState.Running;

        foreach(EnemySpawner spawner in _spawners)
        {
            spawner.StartSpawning();
        }

        _startingSequence = null;
    }

    IEnumerator EndingSequence(LevelState newState)
    {
        yield return new WaitForSecondsRealtime(_waitAfterEndSeconds);
        _state = newState;

        _endingSequence = null;
    }

    void Update()
    {
        if(_state == LevelState.Starting && _startingSequence == null)
        {
            _startingSequence = StartCoroutine(StartingSequence());
        }
        else if(_state == LevelState.Running && _endingSequence == null)
        {
            if(!_home.HasRemainingHealth())
            {
                _endingSequence = StartCoroutine(EndingSequence(LevelState.Failure));
            }
            else if(_spawners.All(s => !s.HasSpawnsRemaining()))
            {
                if(_remainingEnemies == null)
                {
                    _remainingEnemies = FindObjectsOfType<Enemy>();
                }

                if(_remainingEnemies.All(r => r == null) || !_remainingEnemies.Any())
                {
                    _endingSequence = StartCoroutine(EndingSequence(LevelState.Complete));
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
