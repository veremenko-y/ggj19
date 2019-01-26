using UnityEngine;

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
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
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

    void OnCollisionEnter(Collision collision)
    {
        if (hitCoolDown <= 0)
        {
            currentHealth--;
            Debug.Log($"{gameObject.name} health {currentHealth}");
            if (currentHealth <= 0)
            {
                Debug.Log($"{gameObject.name} dead");
                Destroy(gameObject);
            }
            hitCoolDown = 2;
        }
    }
}
