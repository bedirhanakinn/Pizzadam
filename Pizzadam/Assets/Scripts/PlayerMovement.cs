using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 inputVec = movementJoystick.joystickVec;

        if (inputVec != Vector2.zero)
        {
            Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y) * playerSpeed;
            rb.velocity = new Vector3(moveVec.x, rb.velocity.y, moveVec.z); // preserve Y velocity

            // Rotate only if there's movement input
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // stop horizontal movement only
            // Do not rotate if there's no input
        }
    }
}
