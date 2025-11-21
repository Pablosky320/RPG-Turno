using UnityEngine;

public class UnitSelectionToggle : MonoBehaviour
{
    [SerializeField] GameObject[]

        
    // Update is called once per frame
       void  Update()
    {
        Unit unitSelected = UnitSelection.Instance.selectedUnit.name;
        
        if (UnitSelection.Instance == null)
            return;

        var slectedUnit = UnitSelection.Instance.selectedUnit;

        deactivateAllUI();


        switch (unitSelected)
        {

        }
    }

    private void SelectedUnit()
    {
        

    }

    void deactivateAllUI()
    {

    }
}
