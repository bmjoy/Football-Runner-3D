using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GateParam
{
    [SerializeField]
    private Sprite _iconSprite;

    [SerializeField]
    private string _answer;

    [SerializeField]
    private float _weight;

    [SerializeField]
    private bool _isWithText;


    public Sprite IconSprite { get => _iconSprite; }

    public string Answer { get => _answer; }

    public float Weight { get => _weight; }

    public bool IsWithText { get => _isWithText; }
}
