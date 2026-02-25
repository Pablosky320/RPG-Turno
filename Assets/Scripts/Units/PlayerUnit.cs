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

        if (Input.GetMouseButtonDown(0))
            HandleClick();

        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    void HandleClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        if (TurnManager.Instance.currentMode == TurnMode.Move)
            MoveTo(hit.point);

        if (TurnManager.Instance.currentMode == TurnMode.Attack)
        {
            Unit target = hit.collider.GetComponent<Unit>();
            if (target != null && target.faction == Faction.Enemy)
                TryAttack(target);
        }
    }

    void OnMouseDown()
    {
        TurnManager.Instance.SelectUnit(this);
    }
}
