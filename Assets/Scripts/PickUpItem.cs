using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public event PickUp OnPickUp;
    public delegate void PickUp(Item item);


    private void OnTriggerEnter(Collider other)
    {
        DetectItem(other);
        DetectCoin(other);
    }


    private void DetectItem(Collider other)
    {
        var item = other.GetComponentInParent<Item>();

        if (item != null)
            OnDetectItem(item);
    }


    private void OnDetectItem(Item item)
    {
        item.OnPickUp();

        OnPickUp?.Invoke(item);
    }


    private void DetectCoin(Collider other)
    {
        var coin = other.GetComponentInParent<Coin>();

        if (coin != null)
            OnDetectCoin(coin);
    }


    private void OnDetectCoin(Coin coin)
    {
        coin.OnPickUp(); 
    }
}