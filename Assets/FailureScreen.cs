﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailureScreen : MonoBehaviour
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
