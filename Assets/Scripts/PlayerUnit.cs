using UnityEngine;

public class PlayerUnit : Unit
{
    void Update()
    {
        if (TurnManager.Instance.CurrentUnit != this)
            return;

        if (TurnManager.Instance.SelectedUnit != this)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    void OnMouseDown()
    {
        TurnManager.Instance.SelectUnit(this);
    }
}
