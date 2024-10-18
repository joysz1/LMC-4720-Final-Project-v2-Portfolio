using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    private static bool canMove = true;

    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //Input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            animator.SetFloat("Speed", movement.sqrMagnitude);
        } else
        {
            movement.x = 0;
            movement.y = 0;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            animator.SetFloat("Speed", movement.sqrMagnitude);

        }
    }

    void FixedUpdate() 
    {
        //Movement
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        /**
        if (newPosition.x < -5.529) {newPosition.x = -5.529f;}
        if (newPosition.x > 23.46) {newPosition.x = 23.46f;}
        if (newPosition.y > 7.44) {newPosition.y = 7.44f;}
        if (newPosition.y < -4.058) {newPosition.y = -4.058f;}
        */

        rb.MovePosition(newPosition);

    }

    public static void changeMoving()
    {
        canMove = !canMove;
    }
}
