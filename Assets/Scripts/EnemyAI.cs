using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;


public class EnemyAI : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
    }
    // Update is called once per frame
    void Update()
    {
        if (unit.isFriendly) return;

        //si es turno de jugador
        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }
        //si es el turno enemigo y no ha actuado
        if (!unit.hasActed)
        {
            StartCoroutine(DoenemyTurn());
        }
    }

    IEnumerator AttackTarget(Unit target)
    {
        Vector3 lookDir = target.transform.position - transform.position;

        Shooting.Shoot(target.transform.position, attackRange);

        yield return new WaitForSeconds(0.2f);

        unit.FinishAttack();
    }

    bool hasLineOfSight(Unit target)
    {
        return shooting.IsOnLos(target.transform.postion, weaponRange);
    }

    IEnumerator DoenemyTurn()
    {
        //encontrar aliado cercano
        Unit closestPlayerUnit = FindClosestPlayerUnit();

        if (target == null)
        {
            unit.FinishAction();
        }

        if (closestPlayerUnit == null)
        {
            Debug.Log(Unit.characterName + "No encuentra objetivos validos, salta el turno");
            
            Unit.FinishAction();

            yield break;



        }
        float distancetotarget;
        if (distancetotarget = Vector3.Distance(transform.position, target.transform.position))
        {
            yield return AttackTarget(target);
        }
    }

    private Unit FindClosestPlayerUnit()
    {
        Unit closest = null;
        float closestDist = Mathf.Infinity;

        foreach (Unit playerUnit in TurnManager.Instance.playerUnits) ;
        {
            float dist = Vector3.Distance(transform.position, playerUnit.transform.position);
        }
    }

}
