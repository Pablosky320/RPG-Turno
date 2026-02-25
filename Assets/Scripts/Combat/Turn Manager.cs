using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public List<Unit> allUnits = new();
    public List<Unit> turnOrder = new();

    public Unit CurrentUnit { get; private set; }
    public Unit SelectedUnit { get; private set; }

    public TurnMode currentMode = TurnMode.None;

    int currentIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    public void RegisterUnit(Unit unit)
    {
        if (!allUnits.Contains(unit))
            allUnits.Add(unit);
    }

    public void UnregisterUnit(Unit unit)
    {
        allUnits.Remove(unit);
        turnOrder.Remove(unit);
    }

    public void StartCombat()
    {
        turnOrder = new List<Unit>(allUnits);
        turnOrder.Sort((a, b) => b.initiative.CompareTo(a.initiative));

        currentIndex = 0;
        StartTurn();
    }

    void StartTurn()
    {
        currentMode = TurnMode.None;

        if (turnOrder.Count == 0)
            return;

        CurrentUnit = turnOrder[currentIndex];
        CurrentUnit.StartTurn();
    }

    public void EndCurrentTurn()
    {
        currentMode = TurnMode.None;

        currentIndex++;
        if (currentIndex >= turnOrder.Count)
            currentIndex = 0;

        StartTurn();
    }

    public void SelectUnit(Unit unit)
    {
        if (SelectedUnit != null)
            SelectedUnit.SetSelected(false);

        SelectedUnit = unit;
        SelectedUnit.SetSelected(true);

        CombatUIController.Instance.SetSelectedUnit(unit);
    }

    public void SetMode(TurnMode mode)
    {
        currentMode = mode;
    }
}