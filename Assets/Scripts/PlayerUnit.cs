using UnityEngine;

public class PlayerUnit : Unit
{
    Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    void Update()
    {
        if (TurnManager.Instance.CurrentUnit != this)
            return;

        if (TurnManager.Instance.SelectedUnit != this)
            return;

        HandleInput();

        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    void HandleInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        // Attack enemy
        Unit target = hit.collider.GetComponent<Unit>();
        if (target != null && target.faction == Faction.Enemy)
        {
            TryAttack(target);
            return;
        }

        // Move to ground
        MoveTo(hit.point);
    }

    void OnMouseDown()
    {
        TurnManager.Instance.SelectUnit(this);
    }
}
