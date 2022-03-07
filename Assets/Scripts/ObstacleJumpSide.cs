using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionType
{
    Right,
    Left,
    Forward,
    Skip,
    StartFollow
}

public class ObstacleJumpSide : MonoBehaviour
{
    [SerializeField]
    private CollisionType _collisionSide;


    public event Collisioned OnCollisioned;
    public delegate void Collisioned(Character character, CollisionType side);


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
        {
            OnDetect(character);
        }
    }


    private void OnDetect(Character character)
    {
        OnCollisioned?.Invoke(character, _collisionSide);

        Destroy(gameObject);
    }
}