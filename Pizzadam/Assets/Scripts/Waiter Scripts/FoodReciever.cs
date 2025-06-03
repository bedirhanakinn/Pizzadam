using UnityEngine;

public class FoodReceiver : MonoBehaviour
{
    [Header("Expected Food")]
    public TrayManager.FoodType expectedFood;

    [Header("Table Food Meshes")]
    public GameObject cheesePizzaMesh;
    public GameObject pepPizzaMesh;
    public GameObject veggiePizzaMesh;
    public GameObject drinkMesh;

    [Header("Customer Reference")]
    public CustomerManager customer;

    public bool TryReceiveFood(TrayManager.FoodType deliveredFood)
    {
        if (customer == null || deliveredFood != expectedFood || customer.hasReceivedFood)
            return false;

        ShowFoodMesh(deliveredFood);
        customer.StartEating();
        return true;
    }

    private void ShowFoodMesh(TrayManager.FoodType food)
    {
        cheesePizzaMesh.SetActive(false);
        pepPizzaMesh.SetActive(false);
        veggiePizzaMesh.SetActive(false);
        drinkMesh.SetActive(false);

        switch (food)
        {
            case TrayManager.FoodType.CheesePizza:
                cheesePizzaMesh.SetActive(true);
                break;
            case TrayManager.FoodType.PepPizza:
                pepPizzaMesh.SetActive(true);
                break;
            case TrayManager.FoodType.VeggiePizza:
                veggiePizzaMesh.SetActive(true);
                break;
            case TrayManager.FoodType.Drink:
                drinkMesh.SetActive(true);
                break;
        }
    }

    public void SetExpectedFood(TrayManager.FoodType food)
    {
        expectedFood = food;
    }

    public void HideAllFood()
    {
        cheesePizzaMesh.SetActive(false);
        pepPizzaMesh.SetActive(false);
        veggiePizzaMesh.SetActive(false);
        drinkMesh.SetActive(false);
    }
}
