using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierZone : MonoBehaviour
{
    [SerializeField]
    private int _multiplierLevelFactor = 1;

    [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField]
    private Color _startColor;

    [SerializeField]
    private Color _endColor;

    private int _currentLevel;
    private int _bonusCoins;


    private void Start()
    {
        _currentLevel = SLS.Data.Game.Level.Value;

        GameC.Instance.OnLevelEnd += GameC_OnLevelEnd;

        SetNumbers();
    }


    private void OnDestroy()
    {
        if (GameC.Instance != null)
            GameC.Instance.OnLevelEnd -= GameC_OnLevelEnd;
    }


    private void SetNumbers()
    {
        var multipliers = GetComponentsInChildren<Multiplier>();

        for (int i = 0; i < multipliers.Length; i++)
        {
            var number = GetMultiplierNumber(i);
            var color = GetMultiplierColor(i, multipliers.Length);

            multipliers[i].SetUpMultiplier(color, number);

            multipliers[i].OnBallTriggered += MultiplierZone_OnBallTriggered;
        }
    }


    private int GetMultiplierNumber(int i)
    {
        return (_multiplierLevelFactor * _currentLevel) + i;
    }


    private Color GetMultiplierColor(int current, int max)
    {
        var lerpDelta = ((current % max) + 1) / (float)max;

        var part = Color.Lerp(_startColor, _endColor, lerpDelta);

        return part;
    }


    private void MultiplierZone_OnBallTriggered(int bonus)
    {
        if (bonus > _bonusCoins)
            _bonusCoins = bonus;
    }


    private void GameC_OnLevelEnd(bool value)
    {
        MenuUI.Instance.SetWinUICoins(_bonusCoins);

        SLS.Data.Game.Coins.Value += _bonusCoins;
    }


    private void OnTriggerEnter(Collider other)
    {
        DetectBall(other);
    }


    private void DetectBall(Collider other)
    {
        var ball = other.GetComponent<Ball>();

        if (ball != null)
            OnDetect(ball);
    }


    private void OnDetect(Ball ball)
    {
        ball.EndFinishFly();

        _cameraFollow.DeactivateBallTrails();
    }
}