using TMPro;
using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
    private const int SENTINEL_VALUE = 0;

    protected abstract string ItemName { get; }
    protected abstract int ItemCost { get; }
    protected int DeviceCapacity { get; private set; } = SENTINEL_VALUE;
    protected float StepsCooldown { get; private set; } = SENTINEL_VALUE;
    protected float StepsMultiplier { get; private set; } = SENTINEL_VALUE;
    protected float Speed { get; private set; } = SENTINEL_VALUE;
    private bool Purchased { get; set; } = false;

    private void OnEnable()
    {
        TextMeshProUGUI itemName = GetComponentInChildren<TextMeshProUGUI>();
        itemName.text = ItemName;
    }

    protected void SetStats(
        int deviceCapacity=SENTINEL_VALUE,
        float stepsCooldown=SENTINEL_VALUE,
        float stepsMultiplier=SENTINEL_VALUE,
        float speed=SENTINEL_VALUE
        )
    {
        DeviceCapacity = deviceCapacity;
        StepsCooldown = stepsCooldown;
        StepsMultiplier = stepsMultiplier;
        Speed = speed;
    }

    public string GetDescription()
    {
        string description = string.Empty;

        if (Speed != SENTINEL_VALUE)
        {
            description += $"Speed: {Player.Instance.Speed} > {Speed}\n";
        }

        if (StepsCooldown != SENTINEL_VALUE)
        {
            description += $"Steps Cooldown: {Player.Instance.StepsCooldown} > {StepsCooldown}\n";
        }

        if (DeviceCapacity != SENTINEL_VALUE)
        {
            description += $"Device Capacity: {Player.Instance.DeviceCapacity} > {DeviceCapacity}\n";
        }

        if (StepsMultiplier != SENTINEL_VALUE)
        {
            description += $"Steps Multiplier: {Player.Instance.StepsMultiplier} > {StepsMultiplier}\n";
        }   

        return description;
    }

    public string GetName()
    {
        return ItemName;
    }
    
    public int GetCost()
    {
        return ItemCost;
    }

    public string GetPurchaseText()
    {
        return $"Purchase: ({ItemCost} coins)";
    }

    public bool HasSufficientCoins()
    {
        return Player.Instance.Coins >= ItemCost;
    }

    public bool IsPurchased()
    {
        return Purchased;
    }

    public void UpdateItemAsPurchased()
    {
        Purchased = true;
    }

    public bool IsEquipped(ShopItem item)
    {
        string itemName = item.GetName();
        return itemName == Player.Instance.EquippedShoe
            || itemName == Player.Instance.EquippedDevice
            || itemName == Player.Instance.EquippedCircuit;
    }

    public abstract void Equip();
}
