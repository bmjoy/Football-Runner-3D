using System;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right,
    UpRight,
    UpLeft,
    DownRight,
    DownLeft
}

public class InputSystem : MonoSingleton<InputSystem>, IPointerDownHandler, IPointerUpHandler
{
    private class GetCardinalDirections
    {
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);

        public static readonly Vector2 UpRight = new Vector2(1, 1);
        public static readonly Vector2 UpLeft = new Vector2(-1, 1);
        public static readonly Vector2 DownRight = new Vector2(1, -1);
        public static readonly Vector2 DownLeft = new Vector2(-1, -1);
    }

    private Camera mainCamera;
    private Vector3 startTouchPosition;
    private Vector3 endTouchPosition;

    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 deltaMousePosition;

    public Camera MainCamera
    {
        get
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            return mainCamera;
        }
    }

    private bool IsCameraIncluded => MainCamera != null;

    public float Horizontal => deltaMousePosition.x;
    public float Vertical => deltaMousePosition.y;

    public event Action OnTouch;
    public event Action OnRelease;
    public event Action<SwipeData> OnSwipe;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsCameraIncluded)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
            deltaMousePosition = currentMousePosition - previousMousePosition;
            previousMousePosition = currentMousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            previousMousePosition = Vector3.zero;
            currentMousePosition = Vector3.zero;
            deltaMousePosition = Vector3.zero;
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsCameraIncluded)
        {
            startTouchPosition = Input.mousePosition;
            OnTouch?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsCameraIncluded)
        {
            endTouchPosition = Input.mousePosition;
            OnRelease?.Invoke();
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        Vector3 currentSwipe = new Vector3(endTouchPosition.x - startTouchPosition.x, endTouchPosition.y - startTouchPosition.y);
        currentSwipe.Normalize();
        SendSwipe(FindSwipeDirection(currentSwipe));
    }

    private SwipeDirection FindSwipeDirection(Vector2 currentSwipe)
    {
        SwipeDirection direction = SwipeDirection.None;
        if (Vector2.Dot(currentSwipe, GetCardinalDirections.Up) > 0.906f)
        {
            direction = SwipeDirection.Up;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.Down) > 0.906f)
        {
            direction = SwipeDirection.Down;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.Left) > 0.906f)
        {
            direction = SwipeDirection.Left;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.Right) > 0.906f)
        {
            direction = SwipeDirection.Right;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.UpRight) > 0.906f)
        {
            direction = SwipeDirection.UpRight;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.UpLeft) > 0.906f)
        {
            direction = SwipeDirection.UpLeft;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.DownLeft) > 0.906f)
        {
            direction = SwipeDirection.DownLeft;
        }
        else if (Vector2.Dot(currentSwipe, GetCardinalDirections.DownRight) > 0.906f)
        {
            direction = SwipeDirection.DownRight;
        }
        return direction;
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = startTouchPosition,
            EndPosition = endTouchPosition
        };
        OnSwipe?.Invoke(swipeData);
    }
}