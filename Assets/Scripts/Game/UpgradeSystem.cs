using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject trap;

    public int level = 1;
    public int cost = 100;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Trap")
                {
                    if (level < 4)
                    {
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().Points >= cost)
                        {
                            gameObject.transform.localScale += new Vector3(0.5F, 0, 0);
                            GameObject.Find("GameManager").GetComponent<GameManager>().Points -= cost;
                            level++;
                            cost += 100;
                        }
                    }
                }
            }
        }
        
    }
}
