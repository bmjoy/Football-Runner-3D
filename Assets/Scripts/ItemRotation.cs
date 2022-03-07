using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection
{
    X,
    Y,
    Z
}

public class ItemRotation : MonoBehaviour
{
    private const float MAX_OFFSET = 360f;

    [SerializeField]
    private RotationDirection _rotationDirection;

    [SerializeField]
    private Transform _model;

    [SerializeField]
    private float _speed = 100f;

    private float _startOffset;


    private void OnEnable()
    {
        _startOffset = Random.Range(0, MAX_OFFSET);

        SetRotation(_startOffset);
    }


    private void Start()
    {
        StartCoroutine(IERotation());
    }


    private IEnumerator IERotation()
    {
        while (true)
        {
            var speed = _speed * Time.deltaTime;

            SetRotation(speed);

            yield return new WaitForEndOfFrame();
        }
    }


    private void SetRotation(float speed)
    {
        var vec = Vector3.zero;

        switch (_rotationDirection)
        {
            case RotationDirection.X:
                vec = new Vector3(speed, 0f, 0f);

                break;
            case RotationDirection.Y:
                vec = new Vector3(0f, speed, 0f);

                break;
            case RotationDirection.Z:
                vec = new Vector3(0f, 0f, speed);

                break;
        }

        _model.Rotate(vec, Space.Self);
    }
}