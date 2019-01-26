using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenericEnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public int MaxHealth = 3;
    public float HitCollDownTime = 1;
    public float FlashInterval = .05f;

    private int currentHealth;
    private float hitCoolDown;

    private readonly bool flashing = false;
    private float flashCurrentTime;

    private SpriteRenderer renderer;
    private Image[] healthBar;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        currentHealth = MaxHealth;
        healthBar = GetComponentsInChildren(typeof(Image)).Cast<Image>().OrderBy(i => i.name).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < healthBar.Length; i++)
        {
            var c = healthBar[i].color;
            if (currentHealth < (i + 1))
            {
                c.a = 0;
            }
            else
            {
                c.a = 1;
            }
            healthBar[i].color = c;
        }

        if (hitCoolDown > 0)
        {
            hitCoolDown -= Time.deltaTime;
            flashCurrentTime += Time.deltaTime;
            if (flashCurrentTime > FlashInterval)
            {
                flashCurrentTime = 0;
                renderer.enabled = !renderer.enabled;
            }
        }
        else
        {
            renderer.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hitCoolDown <= 0 &&
            other.gameObject.tag == "Trap")
        {
            currentHealth--;
            Debug.Log($"{gameObject.name} health {currentHealth}");
            if (currentHealth <= 0)
            {
                Debug.Log($"{gameObject.name} dead");
                Destroy(gameObject);
            }
            hitCoolDown = HitCollDownTime;
        }
    }
}
