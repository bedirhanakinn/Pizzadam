using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float zigzagSpeed = 5f;
    public float forwardSpeed = 5f;

    private enum Direction { Left, Right }
    private Direction currentDirection = Direction.Right;
    private bool isHolding = false;

    private void Start()
    {
        // Initial straight-down rotation
        transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
    }

    private void Update()
    {
        HandleInput();
        MovePlayer();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
                ToggleDirection();
        }

        isHolding = Input.GetMouseButton(0);
    }

    void MovePlayer()
    {
        // Always move down Z
        Vector3 movement = Vector3.back * forwardSpeed * Time.deltaTime;

        if (!isHolding)
        {
            // Zigzag movement on X
            float xMove = (currentDirection == Direction.Left ? -1f : 1f) * zigzagSpeed * Time.deltaTime;
            movement.x = xMove;

            // Set zigzag rotation
            float zRot = currentDirection == Direction.Left ? -45f : -135f;
            transform.rotation = Quaternion.Euler(-90f, 0f, zRot);
        }
        else
        {
            // Straight movement + rotation
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }

        transform.Translate(movement, Space.World);
    }

    void ToggleDirection()
    {
        currentDirection = (currentDirection == Direction.Left) ? Direction.Right : Direction.Left;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            FindObjectOfType<PizzaGameOverHandler>().HandlePizzaGameOver();
        }
    }



}
