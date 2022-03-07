using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float _moveSpeed;

    [SerializeField]
    protected float _sensivity;

    [SerializeField]
    protected float _limitMove;

    [SerializeField]
    protected float _smoothMoveTime = 0.12f;

    [SerializeField]
    protected Transform _modelToFollow;

    private IMovable _movable;
    private ITouchMovable _touchMovable;

    private Coroutine _previousMovement;
    private Coroutine _previousTouchMove;


    public void ClearBehaviours()
    {
        ChangeMovableBehaviour(new NonMovePattern());
        ExecuteMovement();

        ChangeTouchMovableBehaviour(new NonTouchMovePattern());
        ExecuteTouchMove();
    }


    public void ChangeMovableBehaviour(IMovable movable)
    {
        if (_previousMovement != null)
        {
            _movable.KillTween();

            StopCoroutine(_previousMovement);
        }

        _movable = movable;
    }


    public void ChangeTouchMovableBehaviour(ITouchMovable touchMovable)
    {
        if (_previousTouchMove != null)
        {
            _touchMovable.KillTween();

            StopCoroutine(_previousTouchMove);
        }

        _touchMovable = touchMovable;
    }


    public void ExecuteMovement()
    {
        _previousMovement = StartCoroutine(_movable.Move());
    }


    public void ExecuteTouchMove()
    {
        _previousTouchMove = StartCoroutine(_touchMovable.TouchMove());
    }


    public void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }


    public abstract void OnMinimumWeight();

    protected abstract void InitBehaviours();


    public float MoveSpeed { get => _moveSpeed; }

    public float Sensivity { get => _sensivity; }

    public float LimitMove { get => _limitMove; }

    public float SmoothMoveTime { get => _smoothMoveTime; }

    public Transform ModelToFollow { get => _modelToFollow; }
}