using UnityEngine;

public class TrayManager : MonoBehaviour
{
    public enum FoodType { None, CheesePizza, PepPizza, VeggiePizza, Drink }

    [Header("Tray Food Meshes")]
    public GameObject cheesePizzaObj;
    public GameObject pepPizzaObj;
    public GameObject veggiePizzaObj;
    public GameObject drinkObj;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip serveSound;

    [Header("Win System")]
    public WaiterWinManager winManager; // ✅ Assign in Inspector

    [HideInInspector] public FoodType currentFood = FoodType.None;

    private void Start()
    {
        ClearTray();
    }

    private void OnTriggerEnter(Collider other)
    {
        // --------- Food Pickup Zones ---------
        if (other.CompareTag("CheesePizza"))
        {
            PickupFood(FoodType.CheesePizza);
        }
        else if (other.CompareTag("PepPizza"))
        {
            PickupFood(FoodType.PepPizza);
        }
        else if (other.CompareTag("VeggiePizza"))
        {
            PickupFood(FoodType.VeggiePizza);
        }
        else if (other.CompareTag("Drink"))
        {
            PickupFood(FoodType.Drink);
        }

        // --------- Serve Zone ---------
        else if (other.CompareTag("ServeZone"))
        {
            if (currentFood == FoodType.None)
                return;

            FoodReceiver receiver = other.GetComponent<FoodReceiver>();
            if (receiver != null)
            {
                bool success = receiver.TryReceiveFood(currentFood);
                if (success)
                {
                    PlaySound(serveSound);
                    ClearTray();

                    // ✅ Notify WinManager
                    if (winManager != null)
                    {
                        winManager.RegisterSuccessfulDelivery();
                    }
                }
            }
        }
    }

    private void PickupFood(FoodType food)
    {
        if (food == currentFood) return;

        ClearTray();
        currentFood = food;

        switch (food)
        {
            case FoodType.CheesePizza:
                cheesePizzaObj.SetActive(true);
                break;
            case FoodType.PepPizza:
                pepPizzaObj.SetActive(true);
                break;
            case FoodType.VeggiePizza:
                veggiePizzaObj.SetActive(true);
                break;
            case FoodType.Drink:
                drinkObj.SetActive(true);
                break;
        }

        PlaySound(pickupSound);
    }

    public void ClearTray()
    {
        cheesePizzaObj.SetActive(false);
        pepPizzaObj.SetActive(false);
        veggiePizzaObj.SetActive(false);
        drinkObj.SetActive(false);
        currentFood = FoodType.None;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
