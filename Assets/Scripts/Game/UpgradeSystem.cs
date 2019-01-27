using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    GameManager _gameManager = null;

    [SerializeField]
    float _upgradeScale = 0.25f;
    [SerializeField]
    int _upgradeCostAdd = 10;
    [SerializeField]
    int _startingCost = 50;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Scales funny, cut due to timelimit
        return;
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                GameObject hit = hitInfo.transform.gameObject;
                if(hit.tag == "Trap" &&
                    _gameManager.Points >= _startingCost)
                {
                    hit.transform.localScale += new Vector3(_upgradeScale, 0, _upgradeScale);
                    _gameManager.Points -= _startingCost;
                    _startingCost += _upgradeCostAdd;
                }
            }
        }

    }
}
