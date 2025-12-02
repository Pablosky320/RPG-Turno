using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Playercharacter : MonoBehaviour
{

    float experiece;
    Weapon equippedWeapon;
    Equipment equipment;
    [SerializeField] List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();
    float currentLife;
    float armorValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Earnexperience(float exp)
    {

    }





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

    void IsAlive()
    {
        if (currentLife <= 0)
        {
            Debug.Log(name + "Ha muerto");
        }
    }
}
