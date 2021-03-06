﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    public Button NewGame;
    public string NewGameSceneName;
    public Button Credits;
    public Button Help;
    public Button Exit;
    public Image Title;

    public Image HelpImage;

    public Text CreditsText;
    public int CreditsSpeed = 10;

    private bool creditsRunning = false;
    private bool helpRunning = false;
    private float originalYForCredits;

    void Start()
    {
        NewGame.onClick.AddListener(NewGameOnClick);
        Credits.onClick.AddListener(CreditsOnClick);
        Help.onClick.AddListener(HelpOnClick);
        Exit.onClick.AddListener(ExitOnClick);

        originalYForCredits = CreditsText.rectTransform.position.y;

        MusicPlayer music = FindObjectOfType<MusicPlayer>();
        music.PlayMenu();
    }

    void NewGameOnClick()
    {
        if(NewGameSceneName != null)
        {
            SceneManager.LoadScene(NewGameSceneName);
        }
    }

    void CreditsOnClick()
    {
        creditsRunning = true;
        Credits.enabled = true;
        SetMenuEnable(false);
    }

    void ExitOnClick()
    {
        Application.Quit();
    }

    void HelpOnClick()
    {
        helpRunning = true;
        SetMenuEnable(false);
        HelpImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(helpRunning)
        {
            if(Input.anyKey)
            {
                helpRunning = false;
                SetMenuEnable(true);
                HelpImage.gameObject.SetActive(false);
            }
        }
        if(creditsRunning)
        {
            var p = CreditsText.rectTransform.position;
            p.y = p.y + CreditsSpeed * Time.deltaTime;
            CreditsText.rectTransform.position = p;
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                p.y = originalYForCredits;
                CreditsText.rectTransform.position = p;
                creditsRunning = false;
                SetMenuEnable(true);
            }
        }
    }

    void SetMenuEnable(bool enable)
    {
        Title.enabled = enable;
        NewGame.enabled = enable;
        NewGame.GetComponent<Image>().enabled = enable;
        NewGame.GetComponentInChildren<Text>().enabled = enable;
        Credits.enabled = enable;
        Credits.GetComponent<Image>().enabled = enable;
        Credits.GetComponentInChildren<Text>().enabled = enable;
        Exit.enabled = enable;
        Exit.GetComponent<Image>().enabled = enable;
        Exit.GetComponentInChildren<Text>().enabled = enable;
        Help.enabled = enable;
        Help.GetComponent<Image>().enabled = enable;
        Help.GetComponentInChildren<Text>().enabled = enable;
        Title.enabled = enable;
    }
}
