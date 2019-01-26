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

    public void PlayMenu()
    {
        _source.loop = true;
        _source.clip = _menu;
        _source.Play();
    }
}
