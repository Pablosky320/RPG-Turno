using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Stats")]
    [SerializeField] string characterName;
    public list
    public bool hasActed = true;
    bool hasAtacked = false;
    bool hasMoved = false;
    public bool isFriendly;
    ClickToMove clickToMove;

    //Evita moverse fuera de turno
    public void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        clickToMove.enabled = false;
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
        clickTomove.enabled = false;
        hasMoved = true;
    }
    public void FinishAtack()
    {
        hasActed = true;
    }
    public void FinishAction()
    {
        hasActed = true;
        TurnManager.Instance.TurnEnded();
    }
}