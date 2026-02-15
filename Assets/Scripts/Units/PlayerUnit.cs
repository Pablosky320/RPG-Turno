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

      HandleHover();

        if (TurnManager.Instance.currentMode == TurnMode.None)
            return;

        if (Input.GetMouseButtonDown(0))
            HandleClick();
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

    void HandleHover()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            CombatUIController.Instance.SetHoveredTarget(null);
            return;
        }

        Unit target = hit.collider.GetComponent<Unit>();
        CombatUIController.Instance.SetHoveredTarget(
            target != null && target.faction == Faction.Enemy ? target : null
        );
    }

    void HandleClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        if (TurnManager.Instance.currentMode == TurnMode.Move)
        {
            MoveTo(hit.point);
        }
        else if (TurnManager.Instance.currentMode == TurnMode.Attack)
        {
            Unit target = hit.collider.GetComponent<Unit>();
            if (target != null && target.faction == Faction.Enemy)
            {
                TryAttack(target);
            }
        }
    }

    void OnMouseDown()
    {
        TurnManager.Instance.SelectUnit(this);
    }
}
