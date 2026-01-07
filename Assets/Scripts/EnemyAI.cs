using UnityEngine;
using System.Collections;

public class EnemyAI : Unit
{ 
    public override void StartTurn()
    {
        base.StartTurn();
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine()
    {
        while (currentAP > 0)
        {
            yield return new WaitForSeconds(0.5f);
            currentAP -= 3;
            Debug.Log($"{unitName} attacks player");

            if (currentAP <= 0)
            {
                EndTurn();
                yield break;
            }
        }
    }
}
