using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBehavior : MonoBehaviour
{
    public Button NewGame;
    public string NewGameSceneName;
    public Button Credits;
    public Button Exit;

    public Text CreditsText;
    public int CreditsSpeed = 10;

    private bool creditsRunning = false;
    private float originalYForCredits;

    // Start is called before the first frame update
    void Start()
    {
        NewGame.onClick.AddListener(NewGameOnClick);
        Credits.onClick.AddListener(CreditsOnClick);
        Exit.onClick.AddListener(ExitOnClick);

        originalYForCredits = CreditsText.rectTransform.position.y;
    }

    void NewGameOnClick()
    {
        if (NewGameSceneName != null)
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

    // Update is called once per frame
    void Update()
    {
        if (creditsRunning)
        {
            var p = CreditsText.rectTransform.position;
            p.y = p.y + CreditsSpeed * Time.deltaTime;
            CreditsText.rectTransform.position = p;
            if (Input.GetKeyDown(KeyCode.Escape))
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
        NewGame.enabled = enable;
        NewGame.GetComponent<Image>().enabled = enable;
        NewGame.GetComponentInChildren<Text>().enabled = enable;
        Credits.enabled = false;
        Credits.GetComponent<Image>().enabled = enable;
        Credits.GetComponentInChildren<Text>().enabled = enable;
        Exit.enabled = false;
        Exit.GetComponent<Image>().enabled = enable;
        Exit.GetComponentInChildren<Text>().enabled = enable;
    }
}
