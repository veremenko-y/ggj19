using UnityEngine;
using UnityEngine.AI;

public class MoveToClick : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent _agent = null;
    [SerializeField]
    LayerMask _mask = -1;
    [SerializeField]
    float _castDistance = 100f;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay, out hit, _castDistance, _mask))
            {
                NavMeshHit navHit;
                if(NavMesh.SamplePosition(hit.point, out navHit, _castDistance, _mask))
                {
                    _agent.SetDestination(navHit.position);
                }
            }
        }
    }
}
