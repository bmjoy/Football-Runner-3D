using System.Collections;
using UnityEngine;
using DG.Tweening;
using RootMotion.Demos;
using static RootMotion.Demos.FKOffset;

public enum Dir
{
    X,
    Z
}

public class TouchMovePattern : MonoBehaviour, ITouchMovable
{
    private readonly int _width = Screen.width;

    private const float MULTIPLIER = 2.8f;
    private const float MAX_OFFSET = 30f;
    private const float MIN_OFFSET = -30f;
    private const float BORDER_ERROR = 0.02f;

    private Transform _entityTransform;
    private Tween _moveDirTween;

    private FKOffset[] _fkOffset;

    private Vector3 _entityStartPos;

    private Vector2 _mouseStartPos;
    private Vector2 _mouseLastPos;

    private float _limitMove;
    private float _smoothMoveTime;
    private float _sensivity;

    private float _previousDelta;


    public TouchMovePattern(Transform entityTransform, float smoothMoveTime, float limitMove, float sensivity, FKOffset[] fKOffset)
    {
        _entityTransform = entityTransform;
        _smoothMoveTime = smoothMoveTime;
        _limitMove = limitMove;
        _sensivity = sensivity;

        _fkOffset = fKOffset;
    }


    public IEnumerator TouchMove()
    {
        SetStartPositions();

        while (true)
        {
            var movePos = TouchInput();

            MoveToTouch(movePos);

            yield return null;
        }
    }



    public Vector3 TouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetStartPositions();
        }

        if (Input.GetMouseButton(0))
        {
            if (!Input.GetMouseButtonDown(0))
                ChangeSpine();

            var movePos = CalculateMovePos();

            return movePos;
        }

        _mouseLastPos = Input.mousePosition;

        return Vector3.zero;
    }


    private Vector3 CalculateMovePos()
    {
        var deltaDir = CalcDelta(_mouseStartPos);
        var offsetDir = deltaDir / _width * _sensivity;

        _entityStartPos.y = _entityTransform.position.y;

        var movePos = new Vector3(_entityStartPos.x + offsetDir, _entityStartPos.y, _entityStartPos.z);

        _mouseLastPos = Input.mousePosition;

        return movePos;
    }


    private float CalcDelta(Vector3 mouseStartPos)
    {
        var currentPos = Input.mousePosition;

        return currentPos.x - mouseStartPos.x;
    }


    private void ChangeSpine()
    {
        if (_fkOffset == null) return;

        var deltaDir = CalcDelta(_mouseLastPos);

        for (int i = 0; i < _fkOffset.Length; i++)
        {
            var offset = _fkOffset[i].offsets;

            for (int j = 0; j < offset.Length; j++)
            {
                var x = 0f;
                var y = (deltaDir) * Time.deltaTime * MULTIPLIER;
                var z = deltaDir * Time.deltaTime * MULTIPLIER;

                if (_previousDelta != deltaDir)
                    offset[j].rotationOffset = new Vector3(x, y + offset[j].rotationOffset.y, z + offset[j].rotationOffset.z);

                CheckLimits(offset[j]);
            }

            _fkOffset[i].offsets = offset;
        }

        _previousDelta = deltaDir;
    }


    private void CheckLimits(Offset offset)
    {
        if (offset.rotationOffset.y > MAX_OFFSET)
            offset.rotationOffset.y = MAX_OFFSET;

        if (offset.rotationOffset.y < MIN_OFFSET)
            offset.rotationOffset.y = MIN_OFFSET;

        if (offset.rotationOffset.z > MAX_OFFSET)
            offset.rotationOffset.z = MAX_OFFSET;

        if (offset.rotationOffset.z < MIN_OFFSET)
            offset.rotationOffset.z = MIN_OFFSET;
    }


    private void SetStartPositions()
    {
        _mouseStartPos = Input.mousePosition;
        _entityStartPos = _entityTransform.position;
    }


    public void MoveToTouch(Vector3 movePos)
    {
        var x = movePos.x;

        if (x != 0)
        {
            KillTween();

            _moveDirTween = _entityTransform.DOMoveX(x, _smoothMoveTime);
        }

        CheckBorders();
    }


    private void CheckBorders()
    {
        var x = _entityTransform.position.x;

        if (Mathf.Abs(x) >= _limitMove)
        {
            var movePos = new Vector3(_limitMove - BORDER_ERROR, _entityTransform.position.y, _entityTransform.position.z);

            if (x <= 0)
                movePos.x = -_limitMove + BORDER_ERROR;

            _entityTransform.position = movePos;

            KillTween();
        }
    }


    public void KillTween()
    {
        if (_moveDirTween != null)
        {
            _moveDirTween.Kill();

            _moveDirTween = null;
        }
    }
}