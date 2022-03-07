using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField]
    private Vector2 defaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)]
    [SerializeField]
    private float widthOrHeight = 0;

    private Camera cam;

    private float initialSize;
    private float targetAspect;
    private float initialFov;
    private float horizontalFov = 120f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        initialSize = cam.orthographicSize;
        targetAspect = defaultResolution.x / defaultResolution.y;
        initialFov = cam.fieldOfView;
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
    }

    private void Update()
    {
        if (cam.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / cam.aspect);
            cam.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(horizontalFov, cam.aspect);
            cam.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, widthOrHeight);
        }
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);
        return vFovInRads * Mathf.Rad2Deg;
    }
}