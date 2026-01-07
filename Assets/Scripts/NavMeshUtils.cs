using UnityEngine;
using UnityEngine.AI;
public static class NavMeshUtils
{
    public static float CalculatePathLength(NavMeshAgent agent, Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (!agent.CalculatePath(targetPosition, path))
        {
            return Mathf.Infinity;
        }
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            return Mathf.Infinity;
        }
        float length = 0f;

        for (int i = 1; i < path.corners.Length; i++)
        {
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        return length;
    }
}
