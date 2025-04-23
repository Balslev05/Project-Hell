using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [HideInInspector] public int currency;
    [SerializeField] private int startingCurrency;
    public int bananaPickupValue;

    private void Start()
    {
        currency = startingCurrency;
    }

    public void GetMoney(int amount)
    {
        currency += amount;
    }

    public void LoseMoney(int amount)
    {
        currency -= amount;
    }
}
