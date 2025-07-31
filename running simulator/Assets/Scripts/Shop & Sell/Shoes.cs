using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoes : ShopItem
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
        public float Speed { get; private set; }
        public float StepsMultiplier { get; private set; }

        public Stats(string name, int cost, float speed, float stepsMultiplier)
        {
            Name = name;
            Cost = cost;
            Speed = speed;
            StepsMultiplier = stepsMultiplier;
        }
    }

    private static readonly Dictionary<Tier, Stats> SHOES_STATS_BY_TIER = new()
    {
        { Tier.Tier_1, new Stats(name: "KostKo saran wrap", cost: 50, speed: 2f, stepsMultiplier: 1f) },
        { Tier.Tier_2, new Stats(name: "wooden slippers", cost: 300, speed: 3f, stepsMultiplier: 1f) },
        { Tier.Tier_3, new Stats(name: "cardboard V65", cost: 1000, speed: 3.5f, stepsMultiplier: 1.25f) },
        { Tier.Tier_4, new Stats(name: "asian sandals", cost: 5000, speed : 5f, stepsMultiplier: 1.25f) },
        { Tier.Tier_5, new Stats(name: "average sneakers", cost: 10000, speed : 6f, stepsMultiplier: 1.5f) },
        { Tier.Tier_6, new Stats(name: "Air Force 304s", cost: 25000, speed : 7f, stepsMultiplier: 2f) },
        { Tier.Tier_7, new Stats(name: "Ground Jordam 4s", cost: 60000, speed : 8f, stepsMultiplier: 2f) },
        { Tier.Tier_8, new Stats(name: "track spikes", cost: 100000, speed : 9f, stepsMultiplier: 2.5f) },
        { Tier.Tier_9, new Stats(name: "marathon shoes", cost: 180000, speed : 11f, stepsMultiplier: 3f) },
        { Tier.Tier_10, new Stats(name: "Louis Vitton sigma alpha shoes", cost: 250000, speed : 12f, stepsMultiplier: 3f) },
        { Tier.Tier_11, new Stats(name: "wheelies but the wheel is massive", cost: 350000, speed : 13f, stepsMultiplier: 3.5f) },
        { Tier.Tier_12, new Stats(name: "Galaxy Steppers", cost: 500000, speed : 15f, stepsMultiplier: 4f) },
    };

    [SerializeField] private Tier _tier;
    [SerializeField] private ShopArea _shop;

    protected override string ItemName { get => SHOES_STATS_BY_TIER[_tier].Name; }
    protected override int ItemCost { get => SHOES_STATS_BY_TIER[_tier].Cost; }

    private Button _button;

    private void Start()
    {
        // Used for GetDescription in ShopItem
        SetStats(speed: SHOES_STATS_BY_TIER[_tier].Speed, stepsMultiplier: SHOES_STATS_BY_TIER[_tier].StepsMultiplier);

        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _shop.ViewShopItem(this));
    }

    public override void Equip()
    {
        Player.Instance.SetEquippedShoes(SHOES_STATS_BY_TIER[_tier].Name);
        Player.Instance.SetSpeed(SHOES_STATS_BY_TIER[_tier].Speed);
        Player.Instance.SetStepsMultiplier(SHOES_STATS_BY_TIER[_tier].StepsMultiplier);
    } 
}
