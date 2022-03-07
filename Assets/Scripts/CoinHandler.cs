using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    [SerializeField]
    private int _rewardMultiplier = 2;

    private int _level;


    private void Start()
    {
        InitCoins();

        _level = SLS.Data.Game.Level.Value;
    }


    private void InitCoins()
    {
        var coins = GetComponentsInChildren<Coin>();

        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].OnPickedUp += OnPickUp;
        }
    }


    private void OnPickUp(Coin coin)
    {
        coin.OnPickedUp -= OnPickUp;

        var reward = Random.Range(_level, _level * _rewardMultiplier);

        if (reward <= 0)
            reward = 1;

        SLS.Data.Game.Coins.Value += reward;
    }
}