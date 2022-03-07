using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRig : MonoBehaviour
{
    [SerializeField]
    private Transform _receiveBallPoint;

    [SerializeField]
    private Transform _holdBallPoint;

    [SerializeField]
    private Transform _throwBallPoint;

    [SerializeField]
    private Transform _kickBallPoint;

    [SerializeField]
    private GameObject _skinnedMesh;


    public Transform ThrowBallPoint { get => _throwBallPoint; }

    public Transform ReceiveBallPoint { get => _receiveBallPoint; }

    public Transform HoldBallPoint { get => _holdBallPoint; }

    public Transform KickBallPoint { get => _kickBallPoint; }

    public GameObject SkinnedMesh { get => _skinnedMesh; }
}