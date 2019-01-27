using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenericEnemyBehavior : MonoBehaviour
{
    public int MaxHealth = 3;
    public float HitCollDownTime = 1;
    public float FlashInterval = .05f;

    private int currentHealth;
    private float hitCoolDown;

    private readonly bool flashing = false;
    private float flashCurrentTime;

    private SpriteRenderer spriteRenderer;
    private Image[] healthBar;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = MaxHealth;
        healthBar = GetComponentsInChildren(typeof(Image)).Cast<Image>().OrderBy(i => i.name).ToArray();
    }

    void Update()
    {
        for(var i = 0; i < healthBar.Length; i++)
        {
            var c = healthBar[i].color;
            if(currentHealth < (i + 1))
            {
                c.a = 0;
            }
            else
            {
                c.a = 1;
            }
            healthBar[i].color = c;
        }

        if(hitCoolDown > 0)
        {
            hitCoolDown -= Time.deltaTime;
            flashCurrentTime += Time.deltaTime;
            if(flashCurrentTime > FlashInterval)
            {
                flashCurrentTime = 0;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(hitCoolDown <= 0 &&
            other.gameObject.tag == "Trap")
        {
            HurtEnemy();
            hitCoolDown = HitCollDownTime;
        }
    }

    [Button("Debug Hurt")]
    void HurtEnemy()
    {
        currentHealth--;
        audioSource.pitch = Random.Range(.5f, 1.5f);
        audioSource.Play();
        Debug.Log($"{gameObject.name} health {currentHealth}");
        if(currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} dead");
            Destroy(gameObject);
        }
    }
}
