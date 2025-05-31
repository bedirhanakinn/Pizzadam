using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;

    private Animator animator; // NEW
    private Transform modelTransform; // NEW

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find the child "Model" and get its Animator
        modelTransform = transform.Find("Model");
        if (modelTransform != null)
        {
            animator = modelTransform.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Model child not found!");
        }
    }

    void FixedUpdate()
    {
        Vector2 inputVec = movementJoystick.joystickVec;
        bool isMoving = inputVec != Vector2.zero;

        if (isMoving)
        {
            Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y) * playerSpeed;
            rb.velocity = new Vector3(moveVec.x, rb.velocity.y, moveVec.z); // preserve Y velocity

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // Control animation
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }
}
