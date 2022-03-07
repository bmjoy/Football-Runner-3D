using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextType
{
    Good,
    Bad
}

[System.Serializable]
public struct PerfectTextParam
{
    [SerializeField]
    private GameObject[] _goodText;

    [SerializeField]
    private Transform _goodParent;
    
    [SerializeField]
    private GameObject[] _badText;

    [SerializeField]
    private Transform _badParent;


    public GameObject[] GoodText { get => _goodText; }

    public Transform GoodParent { get => _goodParent; }

    public GameObject[] BadText { get => _badText; }

    public Transform BadParent { get => _badParent; }
}