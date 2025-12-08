using UnityEngine;
using System.Collections;

public static class EnemyAI
{ 
    public static IEnumerator TakeAction(Unit unit)
    {
        // Example: wait a second (pretend the AI is thinking)
        yield return new WaitForSeconds(1f);

        // Example "Attack nearest player unit"
        Debug.Log($"{unit.name} performs an enemy action!");

        // ...perform movement, shooting, animations, etc...
    }
}
