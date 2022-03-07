using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Vertical,
    Horizontal
}

public class ItemFloating : MonoBehaviour
{
    private const float MAX_OFFSET = 1f;

    [SerializeField]
    private Transform _model;

    [SerializeField]
    private Direction _direction;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _distance = 0.2f;

    private float _startOffset;


    private void OnEnable()
    {
        _startOffset = Random.Range(0f, MAX_OFFSET);
    }


    private void Start()
    {
        StartCoroutine(IEFloating());
    }


    private IEnumerator IEFloating()
    {
        while (true)
        {
            Vector3 pos = _model.localPosition;

            var newY = 0f;
            var newX = 0f;

            switch (_direction)
            {
                case Direction.Vertical:
                    newY = Mathf.Sin((Time.time + _startOffset) * _speed);

                    break;
                case Direction.Horizontal:
                    newX = Mathf.Sin((Time.time + _startOffset) * _speed);

                    break;
            }

            _model.localPosition = new Vector3(newX == 0 ? pos.x : newX, newY == 0 ? pos.y : newY, pos.z) * _distance;

            yield return null;
        }
    }
}