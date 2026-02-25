using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;
public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public Faction faction;
    public CombatRole combatRole;

    public int initiative = 10;

    [Header("Health")]
    public int maxHP = 20;
    public int currentHP;

    [Header("Action Points")]
    public int maxAP = 10;
    public int currentAP;

    [Header("Weapon")]
    public float minAttackRange = 0f;
    public float maxAttackRange = 10f;
    public float baseHitChance = 0.75f;
    public float maxRangePenalty = 0.4f;

    [Header("LOS")]
    public LayerMask lineOfSightMask;

    public NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHP = maxHP;
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        while (TurnManager.Instance == null)
            yield return null;

        TurnManager.Instance.RegisterUnit(this);
    }

    public virtual void StartTurn()
    {
        currentAP = maxAP;
    }

    public void EndTurn()
    {
        TurnManager.Instance.EndCurrentTurn();
    }

    public bool MoveTo(Vector3 point)
    {
        if (currentAP <= 0)
            return false;

        currentAP--;
        agent.SetDestination(point);
        return true;
    }

    public bool TryAttack(Unit target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < minAttackRange || distance > maxAttackRange)
            return false;

        if (!HasLineOfSight(target))
            return false;

        float rangePenalty = Mathf.Lerp(0, maxRangePenalty, distance / maxAttackRange);
        float hitChance = baseHitChance - rangePenalty - GetCoverModifier(target);
        hitChance = Mathf.Clamp01(hitChance);

        currentAP -= 4;

        if (Random.value <= hitChance)
            target.TakeDamage(5);

        return true;
    }

    public float PreviewHitChance(Unit target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < minAttackRange || distance > maxAttackRange)
            return 0;

        return Mathf.Clamp01(
            baseHitChance
            - Mathf.Lerp(0, maxRangePenalty, distance / maxAttackRange)
            - GetCoverModifier(target)
        );
    }

    public bool HasLineOfSight(Unit target)
    {
        Vector3 from = transform.position + Vector3.up;
        Vector3 to = target.transform.position + Vector3.up;
        return !Physics.Raycast(from, to - from, Vector3.Distance(from, to), lineOfSightMask);
    }

    public float GetCoverModifier(Unit target)
    {
        Vector3 from = transform.position + Vector3.up;
        Vector3 to = target.transform.position + Vector3.up;

        RaycastHit[] hits = Physics.RaycastAll(from, to - from, Vector3.Distance(from, to));
        float cover = 0f;

        foreach (var hit in hits)
        {
            Cover c = hit.collider.GetComponent<Cover>();
            if (c != null)
                cover = Mathf.Max(cover, c.coverValue);
        }

        return cover;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
            gameObject.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        GetComponent<Renderer>().material.color =
            selected ? Color.green : Color.white;
    }
}