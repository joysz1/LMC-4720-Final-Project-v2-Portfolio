using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() 
    {
        //Movement
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        if (newPosition.x < -8.91) {newPosition.x = -8.91f;}
        if (newPosition.x > 8.9) {newPosition.x = 8.9f;}
        if (newPosition.y > 3.0) {newPosition.y = 3.0f;}
        if (newPosition.y < -4.20) {newPosition.y = -4.20f;}

        rb.MovePosition(newPosition);
    }
}
