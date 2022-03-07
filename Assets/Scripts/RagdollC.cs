using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollC : MonoBehaviour
{
    [SerializeField]
    private AnimatorController _animatorController;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Transform _model;

    private Rigidbody[] _rb;
    private Collider[] _collider;
    private CharacterJoint[] _joint;


    private void Start()
    {
        //InitComponents();
    }


    private void InitComponents()
    {
        _rb = _model.GetComponentsInChildren<Rigidbody>();
        _collider = _model.GetComponentsInChildren<Collider>();
        _joint = _model.GetComponentsInChildren<CharacterJoint>();
    }


    public void TurnOnRagdoll()
    {
        _animator.enabled = false;

        _animatorController.enabled = false;
    }
}