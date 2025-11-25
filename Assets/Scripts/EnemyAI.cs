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
        throw new
    }
    bool hasLineOfSight(Unit target)
    {

    }

    IEnumerator DoenemyTurn()
    {
        //encontrar aliado cercano
        Unit closestPlayerUnit = FindClosestPlayerUnit();

        if (target == null)
        {
            Debug.Log()
        }

        if (closestPlayerUnit == null)
        {
            Debug.Log(Unit.characterName + "No encuentra objetivos validos, salta el turno");
            
            Unit.FinishAction();

            yield break;



        }
        float distancetotarget;
    }

    private Unit FindClosestPlayerUnit()
    {
        Unit closest = null;
        float closest
    }

}
