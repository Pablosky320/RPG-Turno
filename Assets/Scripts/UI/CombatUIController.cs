using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatUIController : MonoBehaviour
{
    public static CombatUIController Instance;

    [Header("Buttons")]
    public Button moveButton;
    public Button attackButton;
    public Button endTurnButton;

    [Header("Info")]
    public TextMeshProUGUI accuracyText;

    Unit selectedUnit;
    Unit hoveredTarget;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        moveButton.onClick.AddListener(OnMoveClicked);
        attackButton.onClick.AddListener(OnAttackClicked);
        endTurnButton.onClick.AddListener(OnEndTurnClicked);
    }

    void Update()
    {
        UpdateAccuracyPreview();
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public void SetHoveredTarget(Unit unit)
    {
        hoveredTarget = unit;
    }

    void UpdateAccuracyPreview()
    {
        if (selectedUnit == null || hoveredTarget == null)
        {
            accuracyText.text = "";
            return;
        }

        float chance = selectedUnit.PreviewHitChance(hoveredTarget);
        accuracyText.text = $"Hit Chance: {(chance * 100f):0}%";
    }

    void OnMoveClicked()
    {
        TurnManager.Instance.SetMode(TurnMode.Move);
    }

    void OnAttackClicked()
    {
        TurnManager.Instance.SetMode(TurnMode.Attack);
    }

    void OnEndTurnClicked()
    {
        TurnManager.Instance.EndCurrentTurn();
    }
}
