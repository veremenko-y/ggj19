using Sirenix.OdinInspector;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject trap;
    GameManager _gameManager = null;

    [SerializeField]
    float _upgradeScale = 0.25f;
    [SerializeField]
    int _upgradeCostAdd = 100;
    [ShowInInspector, ReadOnly]
    int _currentCost = 100;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                GameObject hit = hitInfo.transform.gameObject;
                if(hit.tag == "Trap" &&
                    _gameManager.Points >= _currentCost)
                {
                    hit.transform.localScale += new Vector3(_upgradeScale, 0, _upgradeScale);
                    _gameManager.Points -= _currentCost;
                    _currentCost += _upgradeCostAdd;
                }
            }
        }

    }
}
