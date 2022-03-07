using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallFly : MonoBehaviour
{
    private const float ROTATE_SPEED = 250f;
    private const float INCREASE_SCALE = 1.75f;
    private const float SCALE_TIME = 0.4f;

    private const float TORGUE_POWER_Z = 75f;
    private const float TORGUE_POWER_X = 50f;

    [SerializeField]
    private Transform _ballModel;

    [SerializeField]
    private GameObject[] _flyParticle;

    [SerializeField]
    private Collider[] _collider;

    private Rigidbody _rb;

    private float _previousScale;

    private Coroutine _simulateRotation;


    private void Start()
    {
        ChangeCollidersState(false);

        _rb = GetComponent<Rigidbody>();
    }


    public void StartTargetFly()
    {
        Particles(true);
        ChangeSize(true);
        ChangeCollidersState(true);
        Rotation(true);
    }


    public void EndTargetFly()
    {
        Particles(false);
        ChangeSize(false);
        ChangeCollidersState(false);
        Rotation(false);
    }


    public void StartFinishFly()
    {
        PhysicsRotation();
        Particles(true);
        ChangeSize(true);

        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }


    public void EndFinishFly()
    {
        _rb.constraints = RigidbodyConstraints.None;

        Particles(false);
    }


    private void Rotation(bool isOn)
    {
        if (isOn)
            _simulateRotation = StartCoroutine(IESimulateRotation());
        else
        {
            if (_simulateRotation != null)
                StopCoroutine(_simulateRotation);
        }
    }


    private void PhysicsRotation()
    {
        _rb.AddTorque(Vector3.forward * TORGUE_POWER_Z, ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * TORGUE_POWER_X, ForceMode.Impulse);
    }


    private void Particles(bool isOn)
    {
        for (int i = 0; i < _flyParticle.Length; i++)
            _flyParticle[i].SetActive(isOn);
    }


    private void ChangeCollidersState(bool isOn)
    {
        for (int i = 0; i < _collider.Length; i++)
            _collider[i].enabled = isOn;
    }


    private void ChangeSize(bool start)
    {
        if (start)
        {
            _ballModel.DOScale(INCREASE_SCALE, SCALE_TIME);

            _previousScale = _ballModel.localScale.x;
        }
        else
            _ballModel.DOScale(_previousScale, SCALE_TIME);
    }


    private IEnumerator IESimulateRotation()
    {
        while (true)
        {
            var rotateZ = ROTATE_SPEED * Time.deltaTime;

            _ballModel.Rotate(new Vector3(0, 0, -rotateZ));

            yield return null;
        }
    }
}