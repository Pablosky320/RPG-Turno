using UnityEngine;
using System.Collections;
public class EnemyUnit : Unit
{
    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        EndTurn();
    }
}
