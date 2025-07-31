using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit : ShopItem
{
    private enum Tier
    {
        Tier_1, Tier_2, Tier_3,
    }

    private struct Stats
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }
        public float StepsCooldown { get; private set; }

        public Stats(string name, int cost, float stepsCooldown)
        {
            Name = name;
            Cost = cost;
            StepsCooldown = stepsCooldown;
        }
    }

    private static readonly Dictionary<Tier, Stats> CIRCUIT_STATS_BY_TIER = new()
    {
        { Tier.Tier_1, new Stats(name: "Intel i3-4158U", cost: 75000, stepsCooldown: 1.35f) },
        { Tier.Tier_2, new Stats(name: "Ryzen 5 4500", cost: 100000, stepsCooldown: 1.1f) },
        { Tier.Tier_3, new Stats(name: "Intel i5-9400H", cost: 250000, stepsCooldown: 0.75f) },
    };

    [SerializeField] private Tier _tier;
    [SerializeField] private ShopArea _shop;

    protected override string ItemName { get => CIRCUIT_STATS_BY_TIER[_tier].Name; }
    protected override int ItemCost { get => CIRCUIT_STATS_BY_TIER[_tier].Cost; }

    private Button _button;

    private void Start()
    {
        // Used for GetDescription in ShopItem
        SetStats(stepsCooldown: CIRCUIT_STATS_BY_TIER[_tier].StepsCooldown);

        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => _shop.ViewShopItem(this));
    }

    public override void Equip()
    {
        Player.Instance.SetStepsCooldown(CIRCUIT_STATS_BY_TIER[_tier].StepsCooldown);
        Player.Instance.SetEquippedCircuit(CIRCUIT_STATS_BY_TIER[_tier].Name);
    }
}
