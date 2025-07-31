using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private const int DEGREES_PER_SECOND = 2;

    public static event EventHandler OnPowerUpCollected;

    private enum Tier
    {
        Tier_1, Tier_2, Tier_3,
        Tier_4, Tier_5
    }

    private readonly struct Stats
    {
        public int Buff { get; }
        public int Cooldown { get; }

        public Stats(int buff, int cooldown)
        {
            Buff = buff;
            Cooldown = cooldown;
        }
    }

    private static readonly Dictionary<Tier, Stats> POWERUP_STATS_BY_TIER = new()
    {
        {Tier.Tier_1, new Stats(buff: 10, cooldown: 5) },
        {Tier.Tier_2, new Stats(buff: 100, cooldown: 10) },
        {Tier.Tier_3, new Stats(buff: 800, cooldown: 20) },
        {Tier.Tier_4, new Stats(buff: 1500, cooldown: 25) },
        {Tier.Tier_5, new Stats(buff: 3200, cooldown: 30) },
    };

    private float _cooldownTimer = 0f;
    private bool _onCooldown = false;
    private GameObject _interiorObject;

    [SerializeField] private Tier tier;

    private void Start()
    {
        _interiorObject = gameObject.transform.GetChild(0).gameObject;

        if (_interiorObject == null)
        {
            throw new Exception("Power up missing interior object");
        }
    }

    private void Update()
    {
        // Counterclockwise rotation
        transform.Rotate(new Vector3(0, -DEGREES_PER_SECOND, 0));

        if (_onCooldown)
        {
            _cooldownTimer += Time.deltaTime;
        }

        if (_cooldownTimer > POWERUP_STATS_BY_TIER[tier].Cooldown)
        {
            _interiorObject.SetActive(true);
            _onCooldown = false;
            _cooldownTimer = 0;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isDeviceNotFull = Player.Instance.Steps < Player.Instance.DeviceCapacity;

        if (!_onCooldown && isDeviceNotFull)
        {
            Player.Instance.AddSteps(POWERUP_STATS_BY_TIER[tier].Buff * Player.Instance.StepsMultiplier);

            _onCooldown = true;
            _interiorObject.SetActive(false);
            OnPowerUpCollected?.Invoke(this, EventArgs.Empty);
        }
    }
}
