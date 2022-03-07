using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private const float SCALE_TIME = 0.2f;

    [SerializeField]
    private SpawnParticle _spawnParticle;

    public event PickedUp OnPickedUp;
    public delegate void PickedUp(Coin coin);


    public void OnPickUp()
    {
        OnPickedUp?.Invoke(this);

        _spawnParticle.SpawnParticles();

        Taptic.Light();

        transform.DOScale(Vector3.zero, SCALE_TIME);

        Destroy(gameObject, SCALE_TIME);
    }
}