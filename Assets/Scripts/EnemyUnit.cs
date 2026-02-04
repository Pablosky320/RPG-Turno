using UnityEngine;
using System.Collections;
public class EnemyUnit : Unit
{
    public float attackRange = 2f;
    public int attackAPCost = 4;

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);

        Unit target = FindClosestPlayer();
        if (target == null)
        {
            EndTurn();
            yield break;
        }

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= attackRange)
        {
            TryAttack(target);
        }
        else
        {
            MoveTo(target.transform.position);
        }

        yield return new WaitForSeconds(0.5f);
        EndTurn();
    }

    Unit FindClosestPlayer()
    {
        float minDist = float.MaxValue;
        Unit closest = null;

        foreach (Unit u in FindObjectsOfType<Unit>())
        {
            if (u.faction != Faction.Player || !u.IsAlive)
                continue;

            float d = Vector3.Distance(transform.position, u.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = u;
            }
        }

        return closest;
    }
}
