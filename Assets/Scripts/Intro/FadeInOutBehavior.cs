using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOutBehavior : MonoBehaviour
{
    public float FadeTime = 2;
    public bool Started = false;
    public GameObject NextLogo = null;

    private float fadeTotalTime;
    private State state = State.FadeIn;
    private new SpriteRenderer renderer;
    private FadeInOutBehavior nextScript;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetAlpha(0);
        if (NextLogo != null)
        {
            nextScript = NextLogo.GetComponent<FadeInOutBehavior>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Started) return;
        fadeTotalTime += Time.deltaTime;
        switch (state)
        {
            case State.FadeIn:
                if (fadeTotalTime >= FadeTime)
                {
                    state = State.FadeOut;
                    break;
                }
                SetAlpha(fadeTotalTime / FadeTime);
                break;
            case State.FadeOut:
                if (fadeTotalTime >= FadeTime * 2)
                {
                    state = State.Finish;
                }
                SetAlpha((FadeTime - (fadeTotalTime - FadeTime)) / FadeTime);
                break;
            case State.Finish:
                if (nextScript != null)
                {
                    nextScript.Started = true;
                }
                else
                {
                    // TODO Add scene name
                    SceneManager.LoadScene("Menu");
                }
                break;
        }
    }

    private void SetAlpha(float alpha)
    {
        var c = renderer.color;
        c = new Color(c.r, c.g, c.b, alpha);
        renderer.color = c;
    }

    enum State
    {
        FadeIn = 0,
        FadeOut = 1,
        Finish = 2
    }
}
