using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : Entity
{
    [SerializeField]
    private Progress _progress;

    [SerializeField]
    private BallFly _ballFly;

    private bool _isAvailable;


    private void OnEnable()
    {
        InitBehaviours(); // 1
        InitEvents(); // 2
    }


    public void SetWeight(float weight)
    {
        _progress.SetWeight(weight);
    }


    protected override void InitBehaviours()
    {
        ChangeMovableBehaviour(new NonMovePattern());
    }


    private void InitEvents()
    {
        _progress.OnLowWeight += OnMinimumWeight;
    }


    public void StartFinishFly()
    {
        _ballFly.StartFinishFly();
    }


    public void StartTargetFly()
    {
        _isAvailable = true;

        _ballFly.StartTargetFly();
    }


    public void EndTargetFly()
    {
        _ballFly.EndTargetFly();
    }


    public void EndFinishFly()
    {
        _ballFly.EndFinishFly();
    }


    public void OnEnd(Character character)
    {
        _isAvailable = false;

        EndTargetFly();

        var ballWeight = _progress.CurrentWeight;

        character.ChangeWeight(ballWeight);

        ChangeBehaviours();
    }

    
    private void ChangeBehaviours()
    {
        ChangeTouchMovableBehaviour(new NonTouchMovePattern());
        ExecuteTouchMove();
    }


    public bool IsAvailable()
    {
        return _progress.CurrentWeight > 0 && _isAvailable;
    }


    public override void OnMinimumWeight()
    {
        GameC.Instance.LevelEnd(false);

        ClearBehaviours();
    }
}