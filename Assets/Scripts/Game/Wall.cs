using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    readonly float _wallCooldownSeconds = 3f;

    List<IdTime> stayByEnemies = new List<IdTime>(10);
    SpriteRenderer renderer;
    bool blinkStarted = false;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;
        var id = other.gameObject.GetInstanceID();
        if (!stayByEnemies.Any(i => i.Id == id))
            stayByEnemies.Add(new IdTime(id, 0));
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;
        var id = other.gameObject.GetInstanceID();
        var found = stayByEnemies.FirstOrDefault(i => i.Id == id);
        if (found != null)
        {
            found.Time += Time.deltaTime;
            if (found.Time > _wallCooldownSeconds)
            {
                Destroy(gameObject);
            }
        }
        if (stayByEnemies.Max(i => i.Time) > _wallCooldownSeconds / 2)
        {
            StartCoroutine(Blink(3, .1f));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return;
        var id = other.gameObject.GetInstanceID();
        var found = stayByEnemies.FirstOrDefault(i => i.Id == id);
        if (found != null)
            stayByEnemies.Remove(found);
    }

    IEnumerator Blink(float seconds, float period)
    {
        if (blinkStarted) yield break;
        blinkStarted = true;
        var times = (int)Math.Ceiling(seconds / period);
        while (times-- > 0)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(period);
        }
        renderer.enabled = true;
        blinkStarted = false;
    }

    void Update()
    {
    }

    class IdTime
    {
        public IdTime(int id, float time)
        {
            Id = id;
            Time = time;
        }

        public int Id;
        public float Time;
    }
}
