using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour
{
    public enum CustomerState { Ordering, Eating }
    public CustomerState currentState = CustomerState.Ordering;

    [Header("Food Request Visuals")]
    public GameObject cheesePizzaRequestPrefab;
    public GameObject pepPizzaRequestPrefab;
    public GameObject veggiePizzaRequestPrefab;
    public GameObject drinkRequestPrefab;
    public Transform requestSpawnPoint;

    [Header("Linked Systems")]
    public FoodReceiver foodReceiver;
    public Animator customerAnimator;

    [Header("Settings")]
    public float eatingDuration = 10f;

    private GameObject currentRequestVisual;
    private TrayManager.FoodType currentRequestedFood;

    private void Start()
    {
        StartOrdering();
    }

    public void StartOrdering()
    {
        currentState = CustomerState.Ordering;

        // Set animator state
        if (customerAnimator != null)
        {
            customerAnimator.SetBool("IsEating", false); // Stop eating
            customerAnimator.SetTrigger("Order");        // Start ordering
        }

        // Pick random food
        int rand = Random.Range(1, 5);
        currentRequestedFood = (TrayManager.FoodType)rand;

        // Spawn request visual
        SpawnRequestVisual(currentRequestedFood);

        // Set expected food on the table (FoodReceiver)
        if (foodReceiver != null)
        {
            foodReceiver.SetExpectedFood(currentRequestedFood);
            foodReceiver.HideAllFood(); // Clear any old table visuals
        }
    }

    public void StartEating()
    {
        if (currentState == CustomerState.Eating)
            return;

        currentState = CustomerState.Eating;

        // Destroy request visual
        if (currentRequestVisual != null)
        {
            Destroy(currentRequestVisual);
        }

        // Play eating animation
        if (customerAnimator != null)
        {
            customerAnimator.SetBool("IsEating", true);
        }

        // Wait before reordering
        StartCoroutine(EatingCooldown());
    }

    private IEnumerator EatingCooldown()
    {
        yield return new WaitForSeconds(eatingDuration);
        StartOrdering();
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
