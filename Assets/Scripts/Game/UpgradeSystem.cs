using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject trap;

    public int level = 1;
    public int money = 350;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (level == 1)
        {
            cost = 100;
        }
        if (level == 2)
        {
            cost = 200;
        }
        if (level == 3)
        {
            cost = 400;
        }

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
                        if (money >= cost)
                        {
                            level++;
                            money -= cost;
                        }
                    }
                }
            }
        }
        
    }
}
