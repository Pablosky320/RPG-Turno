
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    public GameObject clickIndicatorPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);

                if (clickIndicatorPrefab != null)
                {
                    GameObject indicator = Instantiate(clickIndicatorPrefab, hit.point, Quaternion.identity);

                    Destroy(indicator, 1.5f);
                }
            }
        }
    }
}
