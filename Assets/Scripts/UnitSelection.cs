using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance;
    public Unit selectedUnit;
   

    private void Awake()
    {
        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    public void ClearSelection()
    {

    }
}
