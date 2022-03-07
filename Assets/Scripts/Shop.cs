using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const string SHOP_ITEM_NAME = "Shop_";

    private List<ShopItem> _shopItem;


    private void Start()
    {
        GameC.Instance.OnLevelLoaded += InitShopItems;

        InitShopItems();
        ShopOnUpgradeEvent();
    }


    private void InitShopItems(int a = 0)
    {
        _shopItem = new List<ShopItem>(GetComponentsInChildren<ShopItem>());

        for (int i = 0; i < _shopItem.Count; i++)
        {
            _shopItem[i].initialize(SHOP_ITEM_NAME + $"{i}");
        }
    }


    private void ShopOnUpgradeEvent()
    {
        for (int i = 0; i < _shopItem.Count; i++)
        {
            _shopItem[i].OnUpgraded += Shop_OnUpgraded;
        }
    }


    private void Shop_OnUpgraded(ShopItem shopItem)
    {
        for (int i = 0; i < _shopItem.Count; i++)
        {
            _shopItem[i].CheckIfAvailableToUpgrade();
        }
    }
}