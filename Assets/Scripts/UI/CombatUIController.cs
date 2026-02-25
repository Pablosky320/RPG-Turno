using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatUIController : MonoBehaviour
{
    public static CombatUIController Instance;

    public Button moveButton;
    public Button attackButton;
    public Button endTurnButton;

    public Slider apBar;
    public TextMeshProUGUI apText;
    public TextMeshProUGUI accuracyText;

    Unit selectedUnit;
    Unit hoveredTarget;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (selectedUnit == null)
            return;

        apBar.maxValue = selectedUnit.maxAP;
        apBar.value = selectedUnit.currentAP;
        apText.text = $"AP {selectedUnit.currentAP}/{selectedUnit.maxAP}";

        if (hoveredTarget != null)
            accuracyText.text =
                $"Hit: {(selectedUnit.PreviewHitChance(hoveredTarget) * 100):0}%";
        else
            accuracyText.text = "";
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void SetHoveredTarget(Unit unit)
    {
        hoveredTarget = unit;
    }

    public void MoveMode() => TurnManager.Instance.SetMode(TurnMode.Move);
    public void AttackMode() => TurnManager.Instance.SetMode(TurnMode.Attack);
    public void EndTurn() => TurnManager.Instance.EndCurrentTurn();
}
