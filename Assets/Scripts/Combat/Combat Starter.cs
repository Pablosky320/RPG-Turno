using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatStarter : MonoBehaviour
{
    IEnumerator Start()
    {
        // Wait one frame to ensure all units registered
        yield return null;

        TurnManager.Instance.StartCombat();
    }
}
