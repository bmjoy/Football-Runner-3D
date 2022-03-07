using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct ShopParticle
{
    [SerializeField]
    private ParticleSystem _success;

    [SerializeField]
    private ParticleSystem _failure;

    [SerializeField]
    private RectTransform _spawnPos;


    public ParticleSystem SuccessParticle { get => _success; }

    public ParticleSystem FailureParticle { get => _failure; }

    public RectTransform SpawnPos { get => _spawnPos; }
}


public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private ShopParticle _shopParticle;

    [SerializeField]
    private Button _buyButton;

    [SerializeField]
    private TextMeshProUGUI _priceText;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private float _startPrice;

    [SerializeField]
    private float _priceMultiplier;

    public event Upgraded OnUpgraded;
    public delegate void Upgraded(ShopItem shopItem);

    private string _key;

    private int _level;
    private int _price;


    public void initialize(string name)
    {
#if UNITY_EDITOR
        //SLS.Data.Game.Coins.Value += 1000000;
#endif

        TrySetLevel(name); // 1

        SetUIParams();

        _key = name;
    }

    /// <summary>
    /// Button UI click
    /// </summary>
    public void TryUpgrade()
    {
        var coins = SLS.Data.Game.Coins.Value;

        if (coins > _price)
        {
            SuccessfulUpgrade();
        }
        else
        {
            UnsuccessfulUpgrade();
        }
    }


    private void SuccessfulUpgrade()
    {
        _level++;

        SLS.Data.Game.Coins.Value -= _price;
        SLS.Data.Shop.UpgradeLevel.Value[_key] = _level;

        SLS.Save();

        ActivateParticle(_shopParticle.SuccessParticle, _shopParticle.SpawnPos);

        SetUIParams();

        Taptic.Light();

        OnUpgraded?.Invoke(this);
    }


    private void UnsuccessfulUpgrade()
    {
        ActivateParticle(_shopParticle.FailureParticle, _shopParticle.SpawnPos);
    }


    private void ActivateParticle(ParticleSystem particle, RectTransform spawnPos)
    {
        if (particle == null || spawnPos == null) return;

        //var camera = CameraFollow.Camera;

        //particle.transform.position = camera.ScreenToWorldPoint(particle.transform.position);

        //particle.gameObject.SetActive(true);
        //particle.Play();
    }


    private void TrySetLevel(string name)
    {
        var dictShop = SLS.Data.Shop.UpgradeLevel.Value;

        if (dictShop.TryGetValue(name, out int value))
        {
            _level = value;
        }
        else
        {
            dictShop.Add(name, 1);

            _level = 1;
        }
    }


    private void SetUIParams()
    {
        SetPrice();
        SetLevel();

        CheckIfAvailableToUpgrade();
    }


    public void CheckIfAvailableToUpgrade()
    {
        var coins = SLS.Data.Game.Coins.Value;
        
        if(coins >= _price)
        {
            _buyButton.interactable = true;
        }
        else
        {
            _buyButton.interactable = false;
        }
    }


    private void SetPrice()
    {
        _price = Mathf.CeilToInt(_startPrice + (_startPrice * _priceMultiplier * _level * _level));

        _priceText.text = _price.ToString();
    }


    private void SetLevel()
    {
        _levelText.text = _level.ToString();
    }
}