using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void ResetUnits(List<unit> units)
    {
        foreach (Unit unit in units)
        {
            Unit.hasActed = false;
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
