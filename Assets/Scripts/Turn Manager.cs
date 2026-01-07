using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private List<Unit> allUnits = new();      // All registered units
    private List<Unit> turnOrder = new();     // Units in turn order
    private int currentIndex = 0;

    public Unit CurrentUnit =>
        (turnOrder.Count > 0 && currentIndex < turnOrder.Count) ? turnOrder[currentIndex] : null;

    public Unit SelectedUnit { get; private set; }

    // Ensure TurnManager exists BEFORE any unit Awake() runs
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("TurnManager");
            Instance = go.AddComponent<TurnManager>();
            DontDestroyOnLoad(go);
        }
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Debug.Log("TurnManager Awake - Instance set");
    }

    #region Unit Registration

    public void RegisterUnit(Unit unit)
    {
        if (unit == null)
            return;

        if (!allUnits.Contains(unit))
            allUnits.Add(unit);

        Debug.Log($"Registered unit: {unit.unitName}");
    }

    public void UnregisterUnit(Unit unit)
    {
        if (allUnits.Contains(unit))
            allUnits.Remove(unit);
    }

    #endregion

    #region Combat Flow

    public void StartCombat()
    {
        if (allUnits.Count == 0)
        {
            Debug.LogError("No units registered! Cannot start combat.");
            return;
        }

        // Filter alive units and sort by initiative
        turnOrder = allUnits
            .Where(u => u != null && u.IsAlive)
            .OrderByDescending(u => u.initiative)
            .ToList();

        if (turnOrder.Count == 0)
        {
            Debug.LogError("No valid units to start combat!");
            return;
        }

        Debug.Log("=== COMBAT START ===");
        foreach (Unit u in turnOrder)
        {
            Debug.Log($"Turn Order: {u.unitName} ({u.faction}) Init:{u.initiative}");
        }

        currentIndex = 0;
        StartTurn();
    }

    void StartTurn()
    {
        if (turnOrder.Count == 0)
            return;

        if (currentIndex >= turnOrder.Count)
        {
            currentIndex = 0;
            Debug.Log("=== NEW ROUND ===");
        }

        Unit unit = turnOrder[currentIndex];

        if (unit == null || !unit.IsAlive)
        {
            currentIndex++;
            StartTurn();
            return;
        }

        // Auto-select player units
        if (unit.faction == Faction.Player)
            SelectUnit(unit);
        else
            SelectedUnit = null;

        unit.StartTurn();
    }

    public void EndCurrentTurn()
    {
        currentIndex++;
        StartTurn();
    }

    public void SelectUnit(Unit unit)
    {
        if (unit == null || unit != CurrentUnit || unit.faction != Faction.Player)
            return;

        if (SelectedUnit != null)
            SelectedUnit.SetSelected(false);

        SelectedUnit = unit;
        SelectedUnit.SetSelected(true);

        Debug.Log($"SELECTED → {unit.unitName}");
    }

    #endregion
}