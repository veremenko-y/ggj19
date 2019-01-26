using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    Button _backToMenu = null;

    [SerializeField]
    string _menuScene = "Menu";

    void Awake()
    {
        _backToMenu.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(_menuScene);
        });
    }
}
