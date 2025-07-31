using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private const float WILDERNESS_BGM_VOLUME = 0.15f;

    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _wildernessBGM;
    [SerializeField] private AudioSource _onCoinsIncreasedSFX;
    [SerializeField] private AudioSource _onPowerUpCollectedSFX;
    [SerializeField] private AudioSource _onEquippedItemSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Player.Instance.OnCoinsChanged += Player_OnCoinsChanged;
        Chest.OnChestOpened += Chest_OnChestOpened;
        PowerUp.OnPowerUpCollected += PowerUp_OnPowerUpCollected;
        ShopArea.OnEquippedItem += Shop_OnPurchaseButtonClicked;
    }

    private void Player_OnCoinsChanged(object sender, Player.OnCoinsChangedEventArgs e)
    {
        if (e.coinsIncreased)
        {
            PlayCoinsIncreasedSFX();
        }
    }

    private void Chest_OnChestOpened(object sender, Chest.OnChestOpenedEventArgs e)
    {
        PlayCoinsIncreasedSFX();
    }

    private void PowerUp_OnPowerUpCollected(object sender, System.EventArgs e)
    {
        PlayPowerUpCollectedSFX();
    }

    private void Shop_OnPurchaseButtonClicked(object sender, System.EventArgs e)
    {
        PlayEquippedItemSFX();
    }

    private void PlayCoinsIncreasedSFX()
    {
        _onCoinsIncreasedSFX.Play();
    }

    private void PlayPowerUpCollectedSFX()
    {
        _onPowerUpCollectedSFX.Play();
    }

    private void PlayEquippedItemSFX()
    {
        _onEquippedItemSFX.Play();
    }

    public IEnumerator FadeBGMOut(float duration)
    {
        float timeElapsed = 0;
        
        while (timeElapsed < duration)
        {
            AudioManager.Instance._wildernessBGM.volume = Mathf.Lerp(WILDERNESS_BGM_VOLUME, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeBGMIn(float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            AudioManager.Instance._wildernessBGM.volume = Mathf.Lerp(0, WILDERNESS_BGM_VOLUME, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
