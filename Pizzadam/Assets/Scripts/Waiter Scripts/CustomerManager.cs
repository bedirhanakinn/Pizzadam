using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour
{
    public enum CustomerState { Sitting = 0, Ordering = 1, Eating = 2 }
    public CustomerState currentState = CustomerState.Sitting;

    [Header("Food Request Visuals")]
    public GameObject cheesePizzaRequestPrefab;
    public GameObject pepPizzaRequestPrefab;
    public GameObject veggiePizzaRequestPrefab;
    public GameObject drinkRequestPrefab;
    public Transform requestSpawnPoint;

    [Header("Linked Systems")]
    public FoodReceiver foodReceiver;
    public Animator customerAnimator;

    [Header("State Durations (in seconds)")]
    public Vector2 sittingTimeRange = new Vector2(5f, 10f);   // Min, Max
    public float eatingDuration = 7f;

    private GameObject currentRequestVisual;
    private TrayManager.FoodType currentRequestedFood;

    private void Start()
    {
        StartCoroutine(SittingState());
    }

    // -------------------- STATES --------------------

    private IEnumerator SittingState()
    {
        currentState = CustomerState.Sitting;
        SetAnimatorState(CustomerState.Sitting);

        float sitTime = Random.Range(sittingTimeRange.x, sittingTimeRange.y);
        yield return new WaitForSeconds(sitTime);

        StartOrdering();
    }

    public void StartOrdering()
    {
        currentState = CustomerState.Ordering;
        SetAnimatorState(CustomerState.Ordering);

        // Pick random food
        int rand = Random.Range(1, 5);
        currentRequestedFood = (TrayManager.FoodType)rand;

        // Show request visual
        SpawnRequestVisual(currentRequestedFood);

        // Set expected food at table
        if (foodReceiver != null)
        {
            foodReceiver.SetExpectedFood(currentRequestedFood);
            foodReceiver.HideAllFood();
        }
    }

    public void StartEating()
    {
        if (currentState != CustomerState.Ordering)
            return;

        currentState = CustomerState.Eating;
        SetAnimatorState(CustomerState.Eating);

        // Remove request visual
        if (currentRequestVisual != null)
        {
            Destroy(currentRequestVisual);
        }

        // Start eating coroutine
        StartCoroutine(EatingCooldown());
    }

    private IEnumerator EatingCooldown()
    {
        yield return new WaitForSeconds(eatingDuration);
        StartCoroutine(SittingState());
    }

    // -------------------- HELPERS --------------------

    private void SetAnimatorState(CustomerState state)
    {
        if (customerAnimator != null)
        {
            customerAnimator.SetInteger("State", (int)state);
        }
    }

    private void SpawnRequestVisual(TrayManager.FoodType food)
    {
        if (requestSpawnPoint == null) return;

        GameObject prefabToSpawn = null;
        switch (food)
        {
            case TrayManager.FoodType.CheesePizza:
                prefabToSpawn = cheesePizzaRequestPrefab;
                break;
            case TrayManager.FoodType.PepPizza:
                prefabToSpawn = pepPizzaRequestPrefab;
                break;
            case TrayManager.FoodType.VeggiePizza:
                prefabToSpawn = veggiePizzaRequestPrefab;
                break;
            case TrayManager.FoodType.Drink:
                prefabToSpawn = drinkRequestPrefab;
                break;
        }

        if (prefabToSpawn != null)
        {
            currentRequestVisual = Instantiate(prefabToSpawn, requestSpawnPoint.position, requestSpawnPoint.rotation, requestSpawnPoint);
        }
    }
}
