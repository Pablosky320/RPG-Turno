using UnityEngine;
using System.Collections;

public class EnemyUnit : Unit
{
    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(AI());
    }

    IEnumerator AI()
    {
        yield return new WaitForSeconds(0.5f);

        Unit target = FindObjectOfType<PlayerUnit>();
        if (target == null)
        {
            EndTurn();
            yield break;
        }

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= maxAttackRange)
            TryAttack(target);
        else
            MoveTo(target.transform.position);

        yield return new WaitForSeconds(0.5f);
        EndTurn();
    }
}