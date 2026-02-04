using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public Faction faction;

    public int maxHP = 20;
    public int currentHP;
    public int initiative = 10;

    public int maxAP = 10;
    public int currentAP;

    public float movementCostPerUnit = 1f;
    public NavMeshAgent agent;

    public bool IsAlive => currentHP > 0;

    public float attackRange = 10f;
    public LayerMask lineOfSightMask;

    protected virtual void Awake()
    {
        if (agent == null)
        agent = GetComponent<NavMeshAgent>();

        currentHP = maxHP;

        // Wait until TurnManager exists before registering
        StartCoroutine(RegisterWithTurnManager());
    }

    private IEnumerator RegisterWithTurnManager()
    {
        // Wait until TurnManager.Instance exists
        while (TurnManager.Instance == null)
            yield return null;

        TurnManager.Instance.RegisterUnit(this);
    }

    public virtual void StartTurn()
    {
        currentAP = maxAP;
        Debug.Log($"TURN START → {unitName} ({faction})");
    }

    public virtual void EndTurn()
    {
        TurnManager.Instance.EndCurrentTurn();
    }

    //Movement

    public bool MoveTo(Vector3 destination)
    {
        float distance = Vector3.Distance(transform.position, destination);
        int cost = Mathf.CeilToInt(distance * movementCostPerUnit);

        if (cost > currentAP)
            return false;

        currentAP -= cost;
        agent.SetDestination(destination);
        return true;
    }

    //Combat

    public bool TryAttack(Unit target)
    {
        if (!target.IsAlive)
            return false;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > attackRange)
        {
            Debug.Log("Target out of range");
            return false;
        }

        if (!HasLineOfSight(target))
        {
            Debug.Log("No line of sight");
            return false;
        }

        float baseChance = 0.75f; // 75%
        float coverPenalty = GetCoverModifier(target);
        float distancePenalty = distance / attackRange * 0.3f;

        float hitChance = baseChance - coverPenalty - distancePenalty;
        hitChance = Mathf.Clamp01(hitChance);

        Debug.Log($"Hit chance: {hitChance * 100f}%");

        if (Random.value <= hitChance)
        {
            Attack(target);
            Debug.Log("HIT");
        }
        else
        {
            Debug.Log("MISS");
        }

        return true;
    }

    public virtual bool Attack(Unit target, int apCost = 4, int damage = 5)
    {
        if (target == null || !target.IsAlive)
            return false;

        if (apCost > currentAP)
            return false;

        currentAP -= apCost;
        target.TakeDamage(damage);

        Debug.Log($"{unitName} attacks {target.unitName} for {damage} damage");
        return true;
    }

    public bool HasLineOfSight(Unit target)
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 targetPos = target.transform.position + Vector3.up * 1.5f;

        Vector3 direction = targetPos - origin;
        float distance = direction.magnitude;

        if (Physics.Raycast(origin, direction.normalized, out RaycastHit hit, distance, lineOfSightMask))
        {
            return hit.collider.GetComponent<Unit>() == target;
        }

        return false;
    }

    public float GetCoverModifier(Unit target)
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 targetPos = target.transform.position + Vector3.up * 1.5f;

        Vector3 direction = targetPos - origin;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(
            origin,
            direction.normalized,
            distance,
            lineOfSightMask
    );
        float highestCover = 0f;

        foreach (RaycastHit hit in hits)
        {
            Cover cover = hit.collider.GetComponent<Cover>();
            if (cover != null)
            {
                highestCover = Mathf.Max(highestCover, cover.coverValue);
            }
        }

        return highestCover;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{unitName} HP: {currentHP}");

        if (currentHP <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log($"{unitName} died!");
        TurnManager.Instance.UnregisterUnit(this);
        gameObject.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        Renderer r = GetComponent<Renderer>();
        if (r != null)
            r.material.color = selected ? Color.green : Color.white;
    }
}