using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private FirstPersonController firstPersonController;

    public class OnStepsChangedEventArgs : EventArgs
    {
        public float stepsToAdd;
    }

    public class OnCoinsChangedEventArgs : EventArgs
    {
        public int coinsToAdd;
        public bool coinsIncreased;
    }

    public event EventHandler<OnStepsChangedEventArgs> OnStepsChanged;
    public event EventHandler OnDeviceCapacityChanged;
    public event EventHandler<OnCoinsChangedEventArgs> OnCoinsChanged;

    public float Steps { get; private set; } = 0;
    public int DeviceCapacity { get; private set; } = 25;
    public float StepsCooldown { get; private set; } = 1.5f;
    public float StepsMultiplier { get; private set; } = 1f;
    public int Coins { get; private set; } = 0;
    public float Speed { get; private set; } = 1f;
    public bool IsInShop { get; private set; } = false;
    public string EquippedShoe { get; private set; } = string.Empty;
    public string EquippedDevice { get; private set; } = string.Empty;
    public string EquippedCircuit { get; private set; } = string.Empty;

    private float _stepsTimer;

    private void OnValidate()
    {
        if (firstPersonController == null)
        { 
            firstPersonController = GetComponent<FirstPersonController>();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if (firstPersonController.IsMoving())
        {
            _stepsTimer += Time.deltaTime;
        }
        
        if (_stepsTimer > StepsCooldown)
        {
            _stepsTimer = 0f;
            AddSteps(Speed * StepsMultiplier);
        }
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AddSteps(float stepsToAdd)
    {
        if (Steps == DeviceCapacity && stepsToAdd > 0)
        {
            return;
        }

        float newAmount = Steps + stepsToAdd;

        if (newAmount < 0)
        {
            Steps = 0;
        }
        else if (newAmount > DeviceCapacity)
        {
            Steps = DeviceCapacity;
        }
        else
        {
            Steps += stepsToAdd;
        }

        OnStepsChanged?.Invoke(this, new OnStepsChangedEventArgs { stepsToAdd = stepsToAdd });
    }

    public void AddCoins(int coinsToAdd)
    {
        Coins += coinsToAdd;
        OnCoinsChanged?.Invoke(this, new OnCoinsChangedEventArgs {
            coinsToAdd = coinsToAdd, coinsIncreased = coinsToAdd > 0
        });
    }

    public void SetDeviceCapacity(int deviceCapacity)
    {
        DeviceCapacity = deviceCapacity;
        OnDeviceCapacityChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetStepsCooldown(float stepsCooldown)
    {
        StepsCooldown = stepsCooldown;
    }

    public void SetStepsMultiplier(float stepsMultiplier)
    {
        StepsMultiplier = stepsMultiplier;
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void SetIsInShop(bool state)
    {
        IsInShop = state;
    }

    public void SetEquippedShoes(string equippedShoe)
    {
        EquippedShoe = equippedShoe;
    }

    public void SetEquippedDevice(string equippedDevice)
    {
        EquippedDevice = equippedDevice;
    }

    public void SetEquippedCircuit(string equippedCircuit)
    {
        EquippedCircuit = equippedCircuit;
    }

    private void OnMovement(InputValue value)
    {
        firstPersonController.moveInput = value.Get<Vector2>();

    }

    private void OnLook(InputValue value)
    {
        firstPersonController.lookInput = value.Get<Vector2>();

    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            firstPersonController.TryJump();
        }
    }
}
