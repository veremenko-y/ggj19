using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string[] _levels = null;
    [SerializeField]
    GameOverScreen _failureScreen = null;
    [SerializeField]
    GameOverScreen _winScreen = null;

    int _nextLevelIndex = 0;
    Level _activeLevel = null;
    Coroutine _loading = null;
    TrapPlacerBehavior _trapPlacer = null;


    public int Points = 0;
    public int BasePoints = 50;


    void Awake()
    {
        _activeLevel = FindObjectOfType<Level>();
        _failureScreen.gameObject.SetActive(false);
        _winScreen.gameObject.SetActive(false);
        _trapPlacer = FindObjectOfType<TrapPlacerBehavior>();
        StartCoroutine(AddMorePoints());
    }

    void Update()
    {
        if((_activeLevel == null || _activeLevel.GetState() == LevelState.Complete) && _loading == null)
        {
            _loading = StartCoroutine(TryLoadNextLevel());
        }

        if(_activeLevel != null && _activeLevel.GetState() == LevelState.Failure)
        {
            Lose();
        }
    }

    IEnumerator TryLoadNextLevel()
    {
        // Unload current level
        if(_activeLevel != null && _loading == null)
        {
            AsyncOperation unloading = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(_activeLevel.gameObject.scene);
            while(!unloading.isDone)
            {
                yield return null;
            }
        }

        // Load next level
        if(_nextLevelIndex < _levels.Length)
        {
            AsyncOperation loading = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_levels[_nextLevelIndex], UnityEngine.SceneManagement.LoadSceneMode.Additive);
            while(!loading.isDone)
            {
                yield return null;
            }

            _activeLevel = FindObjectOfType<Level>();
            if(_activeLevel == null)
            {
                throw new InvalidOperationException("Loaded a scene that does not contain a level!");
            }

            _nextLevelIndex++;
        }
        // No more levels to load
        else
        {
            Win();
        }

        _loading = null;
    }

    void Lose()
    {
        _failureScreen.gameObject.SetActive(true);
        _trapPlacer.gameObject.SetActive(false);
    }

    void Win()
    {
        _winScreen.gameObject.SetActive(true);
        _trapPlacer.gameObject.SetActive(false);
    }

    IEnumerator AddMorePoints()
    {
        while (true)
        {
            if (_activeLevel != null && _activeLevel.GetState() == LevelState.Running)
            {
                Points += BasePoints;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
