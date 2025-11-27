using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;
using UnityEngine.Animations;


public class ClickToMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    Vector3 destination;
    Rigidbody rb;
    public Transform destinoDummie;
    NavMeshAgent agent;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destinoDummie.position;
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        agent.destination = destinoDummie.position;
    }

    // Update is called once per frame

    void Update()
    {
        


        destination = destinoDummie.position;
        if (Input.GetMouseButtonDown(1))
        {
            HandleClick();
            Unit unit = GetComponent<Unit>();
            unit.FinishMovement();
        }

        animator.SetFloat("fowardMovement", agent.velocity.magnitude);

    }

    private void HandleClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            agent.destination = hit.point;
            destinoDummie.position = hit.point;

        }    
    }

    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }


}
