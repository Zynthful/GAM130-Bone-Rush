using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed;

    private Rigidbody rb;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector3 movementDirection;
    private Vector3 yVelocityFix;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetMovementDirection();
        Move();
    }

    void FixedUpdate()
    {

    }

    private void Move()
    {
        yVelocityFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = movementDirection * walkSpeed * Time.deltaTime;
        rb.velocity += yVelocityFix;
    }

    private void GetMovementDirection()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        movementDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }
}
