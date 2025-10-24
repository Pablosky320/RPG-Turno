using System.Collections;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    Vector3 destination;
    Rigidbody rb;
    [SerializeField] Transform destinoDummie;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destination = destinoDummie.position;
    }

    // Update is called once per frame
    void Update()
    {
        destination = destinoDummie.position;
        if (Input.GetMouseButtonDown(1))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        StartCoroutine(MoveToPosition());
    }

    IEnumerator MoveToPosition(Vector3 destination)
    {
        Vector3 moveDirection = destination - transform.position;
        rb.AddForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
        yield return null;
    }
}
