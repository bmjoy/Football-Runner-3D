using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RootMotion.Demos;

public class Character : Entity
{
    private Progress _progress;
    private ChangeAppearance _changeAppearance;
    private BallHandler _ballHandler;
    private AnimatorController _animatorController;
    private SpawnParticle _spawnParticle;
    private PerfectText _perfectText;

    private FKOffset[] _fKOffset;

    private Transform _receiveBallPoint;
    private Transform _holdBallPoint;
    private Transform _throwBallPoint;
    private Transform _kickBallPoint;

    public event Action OnDie;

    private bool _isFirstChange;


    private void OnEnable()
    {
        InitBehaviours(); // 1
        InitComponents(); // 2
        InitEvents(); // 3
    }


    public void ChangeWeight(float weight)
    {
        _progress.ChangeProgress(weight);
    }


    protected override void InitBehaviours()
    {
        ChangeMovableBehaviour(new NonMovePattern());
        ChangeTouchMovableBehaviour(new NonTouchMovePattern());
    }


    private void InitComponents()
    {
        _progress = GetComponent<Progress>();
        _changeAppearance = GetComponent<ChangeAppearance>();
        _ballHandler = GetComponent<BallHandler>();
        _animatorController = GetComponent<AnimatorController>();
        _spawnParticle = GetComponent<SpawnParticle>();
        _perfectText = GetComponent<PerfectText>();

        _fKOffset = GetComponentsInChildren<FKOffset>();
    }


    private void InitEvents()
    {
        _changeAppearance.OnChangedModel += OnModelChange;
        _progress.OnLowWeight += OnMinimumWeight;
        _progress.OnLvlChanged += OnLevelChanged;
    }


    private void OnLevelChanged(bool isUpped)
    {
        if (isUpped)
        {
            _spawnParticle.SpawnParticles(ParticleType.LvlUp);
            _perfectText.ShowPerfectText(TextType.Good);
        }
        else
        {
            _spawnParticle.SpawnParticles(ParticleType.LvlDown);
            _perfectText.ShowPerfectText(TextType.Bad);
        }
    }


    private void OnModelChange(Transform model)
    {
        var characterRig = model.GetComponentInParent<CharacterRig>();

        if (_isFirstChange)
            _animatorController.StartAnim(AnimType.ModelChange);

        if (characterRig.ReceiveBallPoint != null)
        {
            _ballHandler.SetBallParent(null);

            _receiveBallPoint = characterRig.ReceiveBallPoint;
            _holdBallPoint = characterRig.HoldBallPoint;
            _throwBallPoint = characterRig.ThrowBallPoint;
            _kickBallPoint = characterRig.KickBallPoint;

            _ballHandler.SetBallParent(_holdBallPoint);

            MoveBallToPoint();
        }

        _isFirstChange = true;
    }


    public void ThrowBall(Character target)
    {
        _ballHandler.StartThrowSimulation(target, transform);
    }


    public void ReceiveBall(Ball ball)
    {
        _ballHandler.ReceiveBall(ball);

        _changeAppearance.ActivateOutline();

        MoveBallToPoint();
    }


    private void MoveBallToPoint()
    {
        var moveTime = 0.5f;

        _ballHandler.MoveBallToPoint(Vector3.zero, moveTime);
        _ballHandler.RotateBall(Vector3.zero, 0f);
    }


    public void SpawnParticles(ParticleType type)
    {
        _spawnParticle.SpawnParticles(type);
    }


    public void StartAnim(AnimType type, bool value1 = false, bool value2 = false)
    {
        _animatorController.StartAnim(type, value1, value2);
    }


    public void StopAnim(AnimType type, bool value1 = false, bool value2 = false)
    {
        _animatorController.StopAnim(type, value1, value2);
    }


    public void TurnOnContent()
    {
        _changeAppearance.TurnOnContent();
    }


    public void TurnOffContent()
    {
        _changeAppearance.TurnOffContent();
    }


    public float CurrentWeight()
    {
        return _progress.CurrentWeight;
    }


    public AnimatorStateInfo GetCurrentAnim()
    {
        return _animatorController.GetCurrentAnim();
    }


    public void SetAnimMultiplier(float multiplier)
    {
        _animatorController.SetAnimMultiplier(multiplier);
    }


    public override void OnMinimumWeight()
    {
        _animatorController.StartAnim(AnimType.Fall);

        OnDie?.Invoke();

        ClearBehaviours();
    }


    public Transform ReceiveBallPoint { get => _receiveBallPoint; }

    public Transform HoldBallPoint { get => _holdBallPoint; }

    public Transform ThrowBallPoint { get => _throwBallPoint; }

    public Transform KickBallPoint { get => _kickBallPoint; }

    public FKOffset[] FKOffsets { get => _fKOffset; }
}