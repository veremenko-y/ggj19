using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip _menu = null;
    [SerializeField]
    AudioClip _softGame = null;
    [SerializeField]
    AudioClip _hardGame = null;

    AudioSource _source = null;

    void Awake()
    {
        // Singleton
        if(FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        _source = GetComponent<AudioSource>();
    }

    void Play(AudioClip clip)
    {
        if(_source.clip != clip || !_source.isPlaying)
        {
            _source.loop = true;
            _source.clip = clip;
            _source.Play();
        }
    }

    public void PlayMenu()
    {
        Play(_menu);
    }

    public void PlaySoftGame()
    {
        Play(_softGame);
    }

    public void PlayHardGame()
    {
        Play(_hardGame);
    }
}
