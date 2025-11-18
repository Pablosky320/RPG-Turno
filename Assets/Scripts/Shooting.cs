using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Charactermain ")]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TakeDamage(float damage)
    {
        float finalDamage = damage - armorValue;
        currentLife -= finalDamage;
        IsAlive();
    }

    void IsAlive();
    {
        if (currentLife <= 0)
        {
            Debug.Log(name + "Ha muerto")
        }
    }
}
