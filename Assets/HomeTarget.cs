using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HomeTarget : MonoBehaviour
{
    Home _home = null;

    void Awake()
    {
        _home = FindObjectOfType<Home>();
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            int damage = enemy.GetHomeDamage();
            _home.DamageHome(damage);

            if(_home.HasRemainingHealth())
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
