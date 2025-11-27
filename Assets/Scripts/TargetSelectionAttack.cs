using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] EnemyCharacter target_1;
    [SerializeField] EnemyCharacter target_2;
    [SerializeField] Shooting characterShooting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void ShootTarget1()
    {
        characterShooting.shoot();
    }

    void ShootTarget2()
    {
        characterShooting.shoot();
    }
}
