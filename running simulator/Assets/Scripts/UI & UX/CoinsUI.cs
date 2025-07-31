using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    private const int SHOP_POSITION_X = 500;
    private const int SHOP_POSITION_Y = -525;

    private int MainPositionX { get; set; }
    private int MainPositionY { get; set; }
    
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        Player.Instance.OnCoinsChanged += Player_OnCoinsChanged;
        UpdateCoinsText();

        MainPositionX = Mathf.RoundToInt(coinsText.GetComponent<RectTransform>().localPosition.x);
        MainPositionY = Mathf.RoundToInt(coinsText.GetComponent<RectTransform>().localPosition.y);
    }

    private void Player_OnCoinsChanged(object sender, Player.OnCoinsChangedEventArgs e)
    {
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        coinsText.text = $"coins: {Player.Instance.Coins}";
    }

    private void Update()
    {
        if (Player.Instance.IsInShop)
        {
            coinsText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(SHOP_POSITION_X, SHOP_POSITION_Y);
        }
        else
        {
            coinsText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(MainPositionX, MainPositionY);
        }
    }
}
