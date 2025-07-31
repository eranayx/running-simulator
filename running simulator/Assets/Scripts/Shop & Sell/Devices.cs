using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Devices : ShopItem
{
    private enum Tier
    {
        Tier_1, Tier_2, Tier_3,
        Tier_4, Tier_5, Tier_6,
        Tier_7, Tier_8, Tier_9,
        Tier_10, Tier_11, Tier_12
    }

    private struct Stats
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }
        public int DeviceCapacity { get; private set; }

        public Stats(string name, int cost, int deviceCapacity)
        {
            Name = name;
            Cost = cost;
            DeviceCapacity = deviceCapacity;
        }
    }

    private static readonly Dictionary<Tier, Stats> DEVICE_STATS_BY_TIER = new()
    {
        { Tier.Tier_1, new Stats(name: "payphone", cost: 25, deviceCapacity: 100) },
        { Tier.Tier_2, new Stats(name: "telephone", cost: 250, deviceCapacity: 500) },
        { Tier.Tier_3, new Stats(name: "1KB wifi router", cost: 800, deviceCapacity: 2000) },
        { Tier.Tier_4, new Stats(name: "random best buy printer", cost: 1600, deviceCapacity: 5000) },
        { Tier.Tier_5, new Stats(name: "38MB data plan", cost: 4000, deviceCapacity: 10000) },
        { Tier.Tier_6, new Stats(name: "Ground Pod 2 Pro", cost: 10000, deviceCapacity: 16000) },
        { Tier.Tier_7, new Stats(name: "Ding Security Camera", cost: 20000, deviceCapacity: 28000) },
        { Tier.Tier_8, new Stats(name: "Brolex Cosmograph Daytona", cost: 40000, deviceCapacity: 50000) },
        { Tier.Tier_9, new Stats(name: "MacDictionary Air", cost: 100000, deviceCapacity: 100000) },
        { Tier.Tier_10, new Stats(name: "136GB wifi router", cost: 150000, deviceCapacity: 180000) },
        { Tier.Tier_11, new Stats(name: "Daymo Self Driving Kar", cost: 225000, deviceCapacity: 250000) },
        { Tier.Tier_12, new Stats(name: "Simsung Universe S95 Ultra Pro Max", cost: 350000, deviceCapacity: 425000) },
    };

    [SerializeField] private Tier _tier;
    [SerializeField] private ShopArea _shop;

    protected override string ItemName { get => DEVICE_STATS_BY_TIER[_tier].Name; }
    protected override int ItemCost { get => DEVICE_STATS_BY_TIER[_tier].Cost; }

    private Button _button;

    private void Start()
    {
        // Used for GetDescription in ShopItem
        SetStats(deviceCapacity: DEVICE_STATS_BY_TIER[_tier].DeviceCapacity);

        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _shop.ViewShopItem(this));
    }

    public override void Equip()
    {
        Player.Instance.SetDeviceCapacity(DEVICE_STATS_BY_TIER[_tier].DeviceCapacity);
        Player.Instance.SetEquippedDevice(DEVICE_STATS_BY_TIER[_tier].Name);
    }
}
