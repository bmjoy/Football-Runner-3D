using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Side,
    Forward,
    ForwardAndMoveX,
}

public class MoveRoadObstacle : Obstacle
{
    private const float SIDE_DISTANCE = -9f;
    private const float FORWARD_DISTANCE = -23f;

    [SerializeField]
    private float _jumpLength = 3.3f;

    [SerializeField]
    private float _jumpDuration = 0.95f;

    [SerializeField]
    private float _jumpPower = 1.2f;

    [SerializeField]
    private int _jumpCount = 1;

    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private Collider _enterCollider;

    [SerializeField]
    private MoveType _moveType;

    private List<ObstacleJumpSide> _side;


    private void OnEnable()
    {
        InitBehaviours();
        InitObstacleSides();
    }


    private void Start()
    {
        SetUp();
    }


    protected override void InitBehaviours()
    {
        ChangePassableBehaviour(new JumpOverPattern(_jumpDuration, _jumpLength, _jumpPower, _jumpCount));
    }


    private void InitObstacleSides()
    {
        var sides = GetComponentsInChildren<ObstacleJumpSide>();

        _side = new List<ObstacleJumpSide>(sides);

        for (int i = 0; i < sides.Length; i++)
        {
            sides[i].OnCollisioned += OnCollisioned;
        }
    }


    public void StopFollow()
    {
        ChangeFollowableBehaviour(new NonFollowPattern());

        ExecuteFollowable();
    }


    private void OnCollisioned(Character character, CollisionType side)
    {
        switch (side)
        {
            case CollisionType.Forward:
                //JumpOverForward(character);

                break;
            case CollisionType.Skip:
                OnSkip();

                break;
            case CollisionType.StartFollow:
                StartFollow(character);

                break;
        }


        if (side != CollisionType.StartFollow)
            DisableJump();
    }


    private void DisableJump()
    {
        for (int i = 0; i < _side.Count; i++)
        {
            if (_side[i] != null)
                Destroy(_side[i].gameObject);
        }
    }


    private void OnSkip()
    {
        if (_moveType != MoveType.Side) return;

        var follow = new NonFollowPattern();
        ChangeFollowableBehaviour(follow);
    }


    private void StartFollow(Character character)
    {
        IFollowable follow = new NonFollowPattern();

        switch (_moveType)
        {
            case MoveType.Forward:
                follow = new ForwardFollowPlayerPattern(transform, _moveSpeed);

                break;
            case MoveType.ForwardAndMoveX:
                follow = new ForwardAndMoveXFollowPlayerPattern(character.transform, transform, _moveSpeed);

                break;
            case MoveType.Side:
                follow = new SideFollowPlayerPattern(character.transform, transform);

                break;
        }

        ChangeFollowableBehaviour(follow);
        ExecuteFollowable();
    }


    private void SetUp()
    {
        var localPos = new Vector3(_enterCollider.transform.localPosition.x, _enterCollider.transform.localPosition.y, 0f);

        switch (_moveType)
        {
            case MoveType.Forward:
                localPos.z = FORWARD_DISTANCE;

                break;
            case MoveType.ForwardAndMoveX:
                localPos.z = FORWARD_DISTANCE;

                break;
            case MoveType.Side:
                localPos.z = SIDE_DISTANCE;

                break;
        }

        _enterCollider.transform.localPosition = localPos;

        var pos = _enterCollider.transform.position;
        pos.x = 0;

        _enterCollider.transform.position = pos;
    }


    private void JumpOverForward(Character character)
    {
        if (_moveType != MoveType.Side) return;

        var follow = new NonFollowPattern();

        ChangeFollowableBehaviour(follow);
        ExecuteFollowable();

        ExecutePassable(character);
    }


    public MoveType MoveType { get => _moveType; }
}
