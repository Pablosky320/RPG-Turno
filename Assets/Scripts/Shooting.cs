using UnityEngine;

public class Shooting : MonoBehaviour
{
    public void shoot()
    {
        IsOnLos();
    }
    public bool IsOnLos(Transform enemyTransform)
    {
        bool isLos;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, enemyTransform.position, out hit, 100f))
        {
            Character character = hit.collider.GetComponent<Character>();
            
        }
        return false;
    }
}
