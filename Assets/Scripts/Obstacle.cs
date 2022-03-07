using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private IPassable _passable;
    private IFollowable _followable;

    private Coroutine _passableC;
    private Coroutine _followableC;


    public void ChangePassableBehaviour(IPassable passable)
    {
        if (_passableC != null)
            StopCoroutine(_passableC);

        _passable = passable;
    }


    public void ChangeFollowableBehaviour(IFollowable followable)
    {
        if (_followableC != null)
        {
            _followable.KillTween();

            StopCoroutine(_followableC);
        }

        _followable = followable;
    }


    public void ExecutePassable(Character character)
    {
        _passableC = StartCoroutine(_passable.Pass(character));
    }


    public void ExecuteFollowable()
    {
        _followableC = StartCoroutine(_followable.Follow());
    }


    protected abstract void InitBehaviours();
}
