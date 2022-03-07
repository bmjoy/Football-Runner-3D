using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _numberText;

    [SerializeField]
    private MeshRenderer _mesh;

    private Color _color;

    private int _number;

    private bool _isTriggered;

    public event BallTriggered OnBallTriggered;
    public delegate void BallTriggered(int bonus);


    public void SetUpMultiplier(Color color, int number)
    {
        _numberText.text = number.ToString();
        _number = number;

        _color = color;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_isTriggered)
            DetectBall(other);
    }


    private void DetectBall(Collider other)
    {
        var ball = other.GetComponent<Ball>();

        if (ball != null)
            OnDetect();
    }


    private void OnDetect()
    {
        _isTriggered = true;

        OnBallTriggered?.Invoke(_number);

        _mesh.material.color = _color;
    }
}