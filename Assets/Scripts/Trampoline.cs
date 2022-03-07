using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Obstacle
{
    [SerializeField]
    private BoxCollider _boxCollider;

    [SerializeField]
    private float _jumpDuration;

    [SerializeField]
    private float _jumpLength;

    [SerializeField]
    private float _jumpPower;

    [SerializeField]
    private int _jumpCount;


    private void OnEnable()
    {
        InitBehaviours();
    }


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null) 
            OnDetect(character);
    }


    private void OnDetect(Character character)
    {
        _boxCollider.gameObject.SetActive(false);

        ExecutePassable(character);
    }

    protected override void InitBehaviours()
    {
        ChangePassableBehaviour(new JumpOverPattern(_jumpDuration, _jumpLength, _jumpPower, _jumpCount));
    }
}
