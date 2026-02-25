using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Unit unit;

    void Update()
    {
        if (unit == null) return;
        slider.value = unit.currentHP;
        transform.forward = Camera.main.transform.forward;
    }
}