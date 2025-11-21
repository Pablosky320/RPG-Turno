using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] string weaponType;
    [SerializeField] float weaponSpeed;
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponTime;
    [SerializeField] float weaponHealth;

}
