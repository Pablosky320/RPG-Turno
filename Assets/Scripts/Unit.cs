using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public Faction faction;

    public int initiative = 10;
    public int maxAP = 10;
    public int currentAP;

    public bool IsAlive => true;

    protected virtual void Awake()
    {
        // Wait until TurnManager exists before registering
        StartCoroutine(RegisterWithTurnManager());
    }

    private IEnumerator RegisterWithTurnManager()
    {
        // Wait until TurnManager.Instance exists
        while (TurnManager.Instance == null)
            yield return null;

        TurnManager.Instance.RegisterUnit(this);
    }

    public virtual void StartTurn()
    {
        currentAP = maxAP;
        Debug.Log($"TURN START → {unitName} ({faction})");
    }

    public virtual void EndTurn()
    {
        TurnManager.Instance.EndCurrentTurn();
    }

    public void SetSelected(bool selected)
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
            r.material.color = selected ? Color.green : Color.white;
    }
}