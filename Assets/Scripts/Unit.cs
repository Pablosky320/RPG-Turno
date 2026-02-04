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

    [Header("Weapon")]
    public CombatRole combatRole = CombatRole.Ranged;

    public float minAttackRange = 0f;
    public float maxAttackRange = 10f;

    [Range(0f, 1f)]
    public float baseHitChance = 0.75f;

    [Tooltip("How much accuracy is lost at max range")]
    [Range(0f, 1f)]
    public float maxRangePenalty = 0.4f;

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
        if (target == null || !target.IsAlive)
            return false;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Hard range limits
        if (distance < minAttackRange || distance > maxAttackRange)
        {
            Debug.Log("Target out of weapon range");
            return false;
        }

        // Line of sight
        if (!HasLineOfSight(target))
        {
            Debug.Log("No line of sight");
            return false;
        }

        // Distance-based accuracy falloff
        float rangeFactor = distance / maxAttackRange;
        float distancePenalty = Mathf.Lerp(0f, maxRangePenalty, rangeFactor);

        // Cover penalty
        float coverPenalty = GetCoverModifier(target);

        float hitChance = baseHitChance - distancePenalty - coverPenalty;
        hitChance = Mathf.Clamp01(hitChance);

        Debug.Log
        (
            $"{unitName} attack chance: {(hitChance * 100f):0}% " +
            $"(Dist:{distance:0.0}, Cover:{coverPenalty})"
        );

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