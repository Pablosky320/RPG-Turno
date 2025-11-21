using UnityEngine;

public class Shooting : MonoBehaviour
{
    public void shoot(Vector3 enemyPosition, float weaponRange)
    {
        IsOnLos(Vector3.zero, 10f);
    }
    public bool IsOnLos(Transform enemyTransform)
    {
        bool isLos;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, enemyTransform.position, out hit, 100f))
        {
            Character character = hit.collider.GetComponent<Character>();
            
            if (character != null)
            {
                return true;
            }
        }
        return false;
    }
}
