using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public TurnManager Instance;
    public Unit unit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
    }

    private void ResetUnits(List<Unit> units)
    {
        foreach (Unit units in units)
        {
            unit.hasActed = false;
        }
    }

    bool AllUnitsActed(List<Unit> units)
    {
        foreach (var u in units)
        {
            if (!u.hasActed)
            {
                Debug.Log(u.characterName + u.hasActed);
                return false;
            }
        }
    }

    public void CheckEndTurn()
    {
        if (isPlayerTurn)
        {
            if (AllUnitsActed(playerUnits))
            {
                StartEnemyTurn();
            }
        }
        else
        {
            if (AllUnitsActed(enemyUnits))
            {
                StartPlayerTurn();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
    }

    private void StartEnemyTurn()
    {
        isPlayerTurn = false;
        ResetUnits(enemyUnits);
    }
}
