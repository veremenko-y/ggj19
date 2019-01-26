using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string[] _levels = null;

    int _nextLevelIndex = 0;
    Level _activeLevel = null;
    Coroutine _loading = null;
    FailureScreen _failureScreen = null;

    void Awake()
    {
        _activeLevel = FindObjectOfType<Level>();

        _failureScreen = Resources.FindObjectsOfTypeAll<FailureScreen>().First();
        _failureScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if((_activeLevel == null || _activeLevel.GetState() == LevelState.Complete) && _loading == null)
        {
            _loading = StartCoroutine(TryLoadNextLevel());
        }

        if(_activeLevel != null && _activeLevel.GetState() == LevelState.Failure)
        {
            _failureScreen.gameObject.SetActive(true);
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
            GameOver();
        }

        _loading = null;
    }

    void GameOver()
    {
        // TODO handle game over
    }
}
