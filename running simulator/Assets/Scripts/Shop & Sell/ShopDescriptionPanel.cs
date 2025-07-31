using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopDescriptionPanel : MonoBehaviour
{
    [SerializeField] private ShopArea _shop;
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private TextMeshProUGUI _itemDescriptionText;
    [SerializeField] private TextMeshProUGUI _purchaseText;

    private void Start()
    {
        ShopArea.OnShopItemClicked += Shop_OnShopItemClicked;
        ShopArea.OnEquippedItem += Shop_OnPurchaseButtonClicked;
    }

    private void Shop_OnShopItemClicked(object sender, EventArgs e)
    {
        ChangeItemInfoText(sender as ShopItem);
        UpdatePurchaseText(sender as ShopItem);
    }

    private void Shop_OnPurchaseButtonClicked(object sender, EventArgs e)
    {
        UpdatePurchaseText(sender as ShopItem);
    }

    private void ChangeItemInfoText(ShopItem caller)
    {
        List<ShopItem> shopItemsForSaleList = _shop.GetItemsForSaleList();
        int index = shopItemsForSaleList.IndexOf(caller);
        _shop.SetSelectedIndex(index);

        _itemNameText.SetText(caller.GetName());
        _itemDescriptionText.SetText(caller.GetDescription());
    }

    private void UpdatePurchaseText(ShopItem caller)
    {
        if (caller.IsEquipped(caller))
        {
            _purchaseText.SetText("Equipped");
        }
        else if (caller.IsPurchased())
        {
            _purchaseText.SetText("Equip");
        }
        else
        {
            _purchaseText.SetText(caller.GetPurchaseText());
        }
    }
}
