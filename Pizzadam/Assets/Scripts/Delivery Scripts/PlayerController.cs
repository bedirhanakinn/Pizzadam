using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float zigzagSpeed = 5f;
    public float forwardSpeed = 5f;

    [Header("Hold Settings")]
    public float maxHoldDuration = 4f;
    public GameObject holdIndicatorLeft;
    public GameObject holdIndicatorRight;
    public float rotationDuration = 3f;

    [Header("Fade Settings")]
    public float fadeInDuration = 0.5f;

    private Material indicatorMaterialLeft;
    private Material indicatorMaterialRight;

    private GameObject activeIndicator;
    private Material activeMaterial;

    private float holdTimer = 0f;
    private float currentRotationTime = 0f;
    private float fadeTimer = 0f;
    private bool isHolding = false;
    private bool fadingIn = false;

    private enum Direction { Left, Right }
    private Direction currentDirection = Direction.Right;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, -90f);

        if (holdIndicatorLeft != null)
        {
            holdIndicatorLeft.SetActive(false);
            Renderer rend = holdIndicatorLeft.GetComponent<Renderer>();
            if (rend != null)
            {
                indicatorMaterialLeft = rend.material;
                Color c = indicatorMaterialLeft.color;
                c.a = 0f;
                indicatorMaterialLeft.color = c;
            }
        }

        if (holdIndicatorRight != null)
        {
            holdIndicatorRight.SetActive(false);
            Renderer rend = holdIndicatorRight.GetComponent<Renderer>();
            if (rend != null)
            {
                indicatorMaterialRight = rend.material;
                Color c = indicatorMaterialRight.color;
                c.a = 0f;
                indicatorMaterialRight.color = c;
            }
        }
    }

    private void Update()
    {
        HandleInput();
        MovePlayer();
        UpdateHoldIndicatorFadeIn();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                isHolding = true;
                holdTimer = 0f;
                currentRotationTime = 0f;

                // Determine upcoming direction after release
                Direction nextDirection = currentDirection == Direction.Left ? Direction.Right : Direction.Left;

                if (nextDirection == Direction.Left)
                {
                    activeIndicator = holdIndicatorLeft;
                    activeMaterial = indicatorMaterialLeft;
                }
                else
                {
                    activeIndicator = holdIndicatorRight;
                    activeMaterial = indicatorMaterialRight;
                }

                if (activeIndicator != null)
                {
                    activeIndicator.SetActive(true);
                    activeIndicator.transform.localRotation = Quaternion.Euler(0f, -90f, 270f);

                    if (activeMaterial != null)
                    {
                        Color color = activeMaterial.color;
                        color.a = 0f;
                        activeMaterial.color = color;

                        fadeTimer = 0f;
                        fadingIn = true;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                isHolding = false;
                holdTimer = 0f;
                currentRotationTime = 0f;

                if (activeIndicator != null)
                    activeIndicator.SetActive(false);

                ToggleDirection();
            }
        }

        if (isHolding)
        {
            holdTimer += Time.deltaTime;

            if (activeIndicator != null)
            {
                currentRotationTime += Time.deltaTime;
                float progress = Mathf.Clamp01(currentRotationTime / rotationDuration);
                float angleX = (activeIndicator == holdIndicatorLeft)
                    ? Mathf.Lerp(-15f, -50f, progress)
                    : Mathf.Lerp(15f, 50f, progress);

                activeIndicator.transform.localRotation = Quaternion.Euler(angleX, -90f, 270f);
            }

            if (holdTimer >= maxHoldDuration)
            {
                isHolding = false;
                holdTimer = 0f;
                currentRotationTime = 0f;

                if (activeIndicator != null)
                    activeIndicator.SetActive(false);

                ToggleDirection();
            }
        }
    }

    void MovePlayer()
    {
        Vector3 movement = Vector3.back * forwardSpeed * Time.deltaTime;

        if (!isHolding)
        {
            float xMove = (currentDirection == Direction.Left ? -1f : 1f) * zigzagSpeed * Time.deltaTime;
            movement.x = xMove;

            float zRot = currentDirection == Direction.Left ? -45f : -135f;
            transform.rotation = Quaternion.Euler(-90f, 0f, zRot);
        }
        else
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }

        transform.Translate(movement, Space.World);
    }

    void ToggleDirection()
    {
        currentDirection = (currentDirection == Direction.Left) ? Direction.Right : Direction.Left;
    }

    void UpdateHoldIndicatorFadeIn()
    {
        if (fadingIn && activeMaterial != null)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeInDuration);

            Color color = activeMaterial.color;
            color.a = alpha;
            activeMaterial.color = color;

            if (alpha >= 1f)
            {
                fadingIn = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            FindObjectOfType<PizzaGameOverHandler>().HandlePizzaGameOver();
        }
    }
}
