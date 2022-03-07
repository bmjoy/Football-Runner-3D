using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ItemRotation), typeof(ItemFloating))]
public class Item : MonoBehaviour
{
    private const float SCALE_TIME = 0.6f;

    [SerializeField]
    private SpawnParticle _spawnParticle;

    [SerializeField]
    private GameObject _icon;

    [SerializeField]
    private Collider[] _collider;

    [SerializeField]
    private float _weight;


    public void OnPickUp()
    {
        for (int i = 0; i < _collider.Length; i++)
            _collider[i].enabled = false;

        _spawnParticle.SpawnParticles();

        Taptic.Light();

        _icon.SetActive(false);

        transform.DOScale(Vector3.zero, SCALE_TIME);

        Destroy(gameObject, SCALE_TIME);
    }


    public float Weight { get => _weight; }
}