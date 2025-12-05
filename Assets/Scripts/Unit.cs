using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    [Header("Unit Stats")]
    public string characterName;
    public List<Unit> units= new List<Unit>();
    public bool hasActed = true;

    bool hasAtacked = false;
    bool hasMoved = false;
    public bool isFriendly;

    ClickToMove clickToMove;
    TurnManager turnManager;    
    public float AP;
    //Evita moverse fuera de turno
    public void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        clickToMove.enabled = false;
        turnManager = GetComponent<TurnManager>();
    }
    public void Run()
    {
        if (hasMoved || hasAtacked) //Acaba la funcion si ha actuado o atacado
        {
            return;
        }
        if (isFriendly == true)
        {
            clickToMove.enabled = true;
            clickToMove.destinoDummie.position = transform.position;
            Debug.Log(characterName + "se ha movido.");
        }
        else
        {
            Debug.Log(characterName + "no se ha movido");
        }
    }
    public void Atack()
    {
        if ((hasActed = true) && (hasAtacked = true))
        {
            return;
        }
        Debug.Log(characterName + "esta liandose a piñas");
        FinishAtack();
    }
    public void EndTurn()
    {
        if (hasActed)
        {
            return;
        }
        Debug.Log(characterName + "dice que ya no puede mas");
        FinishAction();
    }
    public void FinishMovement()
    {
        clickToMove.enabled = false;
        hasMoved = true;
    }
    public void FinishAtack()
    {
        hasActed = true;
    }
    public void FinishAction()
    {
        hasActed = true;
        turnManager.Instance.TurnEnded();
    }
}