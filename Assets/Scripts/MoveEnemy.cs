using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private const float MINIMUM_MOVE_SPEED = 0.65f;

    [SerializeField]
    private AnimatorController _animatorController;

    [SerializeField]
    private MoveRoadObstacle _moveRoadObstacle;

    [SerializeField]
    private AttackAffector _attackAffector;

    [SerializeField]
    private SpawnParticle _spawnParticle;

    private Vector3 _oldPosition;

    private bool _isRun;


    private void Start()
    {
        _oldPosition = transform.position;

        _animatorController.StartAnim(AnimType.CrouchIdle);

        InitEvents();
    }


    private void Update()
    {
        CheckSpeed();
    }


    private void CheckSpeed()
    {
        if (GetSpeed() > MINIMUM_MOVE_SPEED)
            StartRun();
        else
            StartIdle();
    }


    private void StartRun()
    {
        if (_isRun) return;

        _isRun = true;

        var moveType = _moveRoadObstacle.MoveType;

        switch (moveType)
        {
            case MoveType.Side:
                _animatorController.StartAnim(AnimType.CrouchWalk, true);

                break;
            case MoveType.Forward:
                _animatorController.StartAnim(AnimType.Run, true);

                break;
            case MoveType.ForwardAndMoveX:
                _animatorController.StartAnim(AnimType.Run, true);

                break;
        }
    }


    private void InitEvents()
    {
        if (_attackAffector == null) Debug.LogWarning("AttackEffector null");

        _attackAffector.OnTouched += OnTouchedCharacter;
    }


    private void OnTouchedCharacter(Character character, bool isMoreWeight)
    {
        if (isMoreWeight)
        {
            _animatorController.StartAnim(AnimType.Fall);

            _attackAffector.StopFollow();
        }
        else
            _animatorController.StartAnim(AnimType.Idle);

        _moveRoadObstacle.StopFollow();

        _spawnParticle.SpawnParticles(ParticleType.Collision);

        Taptic.Medium();
    }


    private void StartIdle()
    {
        if (!_isRun) return;

        _isRun = false;

        _animatorController.StartAnim(AnimType.CrouchWalk, false);
    }


    private float GetSpeed()
    {
        var a = _oldPosition;
        var b = transform.position;

        var distance = (a - b).sqrMagnitude;

        _oldPosition = transform.position;

        var speed = distance / (Time.deltaTime * Time.deltaTime);

        return Mathf.Abs(speed);
    }
}