using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Transform to follow")] [SerializeField] private Transform target;
    [Tooltip("Current relative offset to the target")] [SerializeField] private Vector3 offset;
    [Tooltip("Smooth follow speed")] [Range(0f, 4f)] [SerializeField] private float smoothSpeed = 0.125f;

    private float targetCamSize;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetCamSize = cam.orthographicSize;
    }

    private void FixedUpdate()
    {
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, target.position.z + offset.z);
    }

    private void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCamSize, Time.deltaTime * 2f);
    }

    public void Win() => targetCamSize = 3f;
}