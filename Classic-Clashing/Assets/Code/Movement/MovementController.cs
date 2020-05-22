using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;
    [SerializeField] Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movementHorizontal = transform.right * xMovement;
        Vector3 movementVertical = transform.forward * zMovement;

        Vector3 movementVelocity = (movementHorizontal + movementVertical).normalized * speed;
        Move(movementVelocity);

    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            Vector3 moveTo = rb.position + velocity * Time.fixedDeltaTime;
            rb.MovePosition(moveTo);
        }
    }

    private void Move(Vector3 dir)
    {
        velocity = dir;
    }
}
