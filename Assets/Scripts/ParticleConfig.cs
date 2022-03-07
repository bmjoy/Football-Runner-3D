using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    Landing,
    LvlUp,
    LvlDown,
    Collision,
    Good,
    Bad
}

[System.Serializable]
public struct ParticleConfig
{
    [SerializeField]
    private GameObject[] _particle;

    [SerializeField]
    private Transform[] _spawnPlace;

    [SerializeField]
    private float _lifeTime;

    [SerializeField]
    private ParticleType _type;


    public GameObject[] Particle { get => _particle; }

    public Transform[] SpawnPlace { get => _spawnPlace; }

    public float LifeTime { get => _lifeTime; }

    public ParticleType Type { get => _type; }
}