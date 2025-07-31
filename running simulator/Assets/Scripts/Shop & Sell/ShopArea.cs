using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopArea : MonoBehaviour
{
    public static event EventHandler OnShopItemClicked;
    public static event EventHandler OnEquippedItem;

    [SerializeField] private GameObject _shopScreen;
    [SerializeField] private List<GameObject> _itemsForSale;

    private readonly List<ShopItem> _itemsForSaleList = new();
    private int _selectedIndex = 0;

    private void Start()
    {
        foreach (GameObject item in _itemsForSale)
        {
            if (item.TryGetComponent<ShopItem>(out ShopItem shopItem))
            {
                _itemsForSaleList.Add(shopItem);
            }
        }
    }

    public List<ShopItem> GetItemsForSaleList()
    {
        return _itemsForSaleList;
    }

    public void SetSelectedIndex(int index)
    {
        if (index < _itemsForSaleList.Count)
        {
            _selectedIndex = index;
        }
        else
        {
            throw new IndexOutOfRangeException("Selected index out of itemsForSale list range");
        }
    }

    public int GetSelectedIndex()
    {
        return _selectedIndex;
    }

    public void ViewShopItem(ShopItem caller)
    {
        OnShopItemClicked?.Invoke(caller, EventArgs.Empty);
    }

    public void PurchaseSelectedItem()
    {
        ShopItem itemToPurchase = _itemsForSaleList[_selectedIndex];

        if (itemToPurchase.HasSufficientCoins() && !itemToPurchase.IsPurchased())
        {
            Debug.Log(itemToPurchase.GetCost());
            Player.Instance.AddCoins(itemToPurchase.GetCost() * -1);
            itemToPurchase.UpdateItemAsPurchased();
        }

        if (itemToPurchase.IsPurchased())
        {
            itemToPurchase.Equip();
            OnEquippedItem?.Invoke(_itemsForSaleList[_selectedIndex], EventArgs.Empty);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.UnlockCursor();
        Player.Instance.SetIsInShop(true);
        _shopScreen.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.LockCursor();
        Player.Instance.SetIsInShop(false);
        _shopScreen.SetActive(false);
    }
}
