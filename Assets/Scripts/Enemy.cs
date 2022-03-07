using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MINIMUM_MOVE_SPEED = 1f;

    [SerializeField]
    private AnimatorController _animatorController;

    [SerializeField]
    private AttackAffector _attackAffector;

    [SerializeField]
    private SpawnParticle _spawnParticle;

    [SerializeField]
    private AnimType _startAnim;

    private bool _isRun;

    private Vector3 _oldPosition;


    private void Start()
    {
        StartAnim();
        InitEvents();
    }


    private void Update()
    {
        ChangeAnims();
    }


    private void StartAnim()
    {
        if (_startAnim == AnimType.None) return;

        _animatorController.StartAnim(_startAnim);
    }


    private void StartRun()
    {
        _isRun = true;

        _animatorController.StartAnim(AnimType.Walk, true);
    }


    private void StartIdle()
    {
        _isRun = false;

        _animatorController.StartAnim(AnimType.Walk, false);
    }


    private void ChangeAnims()
    {
        if (CheckSpeed() && !_isRun)
            StartRun();
        else if (!CheckSpeed() && _isRun)
            StartIdle();
    }


    private bool CheckSpeed()
    {
        if (GetSpeed() > MINIMUM_MOVE_SPEED)
            return true;
        else
            return false;
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

        _spawnParticle.SpawnParticles(ParticleType.Collision);

        Taptic.Medium();
    }


    public void SetTarget(Character character)
    {
        _attackAffector.SetTarget(character);
    }


    public void StartFollow()
    {
        _attackAffector.StartFollow();
    }


    public void StopFollow()
    {
        _attackAffector.StopFollow();
    }


    private float GetSpeed()
    {
        var a = _oldPosition;
        var b = transform.position;

        var distance = (a - b).sqrMagnitude;

        _oldPosition = transform.position;

        return distance / (Time.deltaTime * Time.deltaTime);
    }
}
