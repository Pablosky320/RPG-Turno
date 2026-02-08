using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    Unit unit;

    public void Init(Unit u)
    {
        unit = u;
        slider.maxValue = u.maxHP;
        slider.value = u.currentHP;
    }

    void Update()
    {
        if (unit == null)
            return;

        slider.value = unit.currentHP;
        transform.rotation = Camera.main.transform.rotation;
    }
}
