using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatStarter : MonoBehaviour
{
    void Start()
    {
        TurnManager.Instance.StartCombat();
    }
}
