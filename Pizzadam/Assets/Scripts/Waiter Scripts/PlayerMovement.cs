using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float playerSpeed = 5f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;

    private Animator animator;
    private Transform modelTransform;

    private AudioSource walkingAudioSource; // NEW

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        modelTransform = transform.Find("Model");
        if (modelTransform != null)
        {
            animator = modelTransform.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Model child not found!");
        }

        // Get AudioSource component
        walkingAudioSource = GetComponent<AudioSource>(); // NEW
        if (walkingAudioSource == null)
        {
            Debug.LogError("Missing AudioSource component on Player!");
        }
    }

    void FixedUpdate()
    {
        Vector2 inputVec = movementJoystick.joystickVec;
        bool isMoving = inputVec != Vector2.zero;

        if (isMoving)
        {
            Vector3 moveVec = new Vector3(inputVec.x, 0, inputVec.y) * playerSpeed;
            rb.velocity = new Vector3(moveVec.x, rb.velocity.y, moveVec.z);

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(inputVec.x, 0, inputVec.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Play walking sound
            if (walkingAudioSource != null && !walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Play();
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // Stop walking sound
            if (walkingAudioSource != null && walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
            }
        }

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }
}
