using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public class OnChestOpenedEventArgs : EventArgs
    {
        public Chest caller;
        public Collider other;
    }

    public static event EventHandler<OnChestOpenedEventArgs> OnChestOpened;

    private enum Tier
    {
        Tier_1, Tier_2, Tier_3,
    }

    private readonly struct ChestReward
    {
        public int Coins { get; }

        public ChestReward(int coins)
        {
            Coins = coins;
        }
    }

    private static readonly Dictionary<Tier, ChestReward> CHEST_REWARD_BY_TIER = new()
    {
        {Tier.Tier_1, new ChestReward(coins: 500) },
        {Tier.Tier_2, new ChestReward(coins: 10000) },
        {Tier.Tier_3, new ChestReward(coins: 40000) },
    };

    private bool _isCollected = false;
    [SerializeField] private Tier tier;

    public bool AnimationPlayed { get; private set; } = false;

    private void Start()
    {
        OnChestOpened += Chest_OnChestOpened;
    }

    private void Chest_OnChestOpened(object sender, OnChestOpenedEventArgs e)
    {
        CollectChest(e.caller, e.other);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnChestOpened?.Invoke(this, new OnChestOpenedEventArgs { caller = this, other = other });
    }

    private void CollectChest(Chest caller, Collider other)
    {
        if (!caller._isCollected && other.CompareTag("Player"))
        {
            caller.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            caller.gameObject.GetComponentInChildren<BoxCollider>().isTrigger = false;
            caller._isCollected = true;

            Player.Instance.AddCoins(CHEST_REWARD_BY_TIER[caller.tier].Coins);
        }
    }

    public void SetAnimationAsPlayed()
    {
        AnimationPlayed = true;
    }
}
