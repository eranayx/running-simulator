using TMPro;
using UnityEngine;

public class StepsUI : MonoBehaviour
{
    private const int SHOP_POSITION_X = -350;
    private const int SHOP_POSITION_Y = -525;

    private int MainPositionX { get; set; }
    private int MainPositionY { get; set; }

    [SerializeField] private TextMeshProUGUI stepsText;

    private void Start()
    {
        Player.Instance.OnStepsChanged += Player_OnStepsChanged;
        Player.Instance.OnDeviceCapacityChanged += Player_OnDeviceCapacityChanged;
        UpdateStepsText();

        MainPositionX = Mathf.RoundToInt(stepsText.GetComponent<RectTransform>().localPosition.x);
        MainPositionY = Mathf.RoundToInt(stepsText.GetComponent<RectTransform>().localPosition.y);
    }

    private void Player_OnStepsChanged(object sender, Player.OnStepsChangedEventArgs e)
    {
        UpdateStepsText();
    }

    private void Player_OnDeviceCapacityChanged(object sender, System.EventArgs e)
    {
        UpdateStepsText();
    }

    private void UpdateStepsText()
    {
        stepsText.text = $"steps: {Player.Instance.Steps} / {Player.Instance.DeviceCapacity}";
    }

    private void Update()
    {
        if (Player.Instance.IsInShop)
        {
            stepsText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(SHOP_POSITION_X, SHOP_POSITION_Y);
        }
        else
        {
            stepsText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(MainPositionX, MainPositionY);
        }
    }
}
