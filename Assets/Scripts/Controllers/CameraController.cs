using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Transform to follow")] [SerializeField] private Transform target;
    [Tooltip("Current relative offset to the target")] [SerializeField] private Vector3 offset;
    [Tooltip("Smooth follow speed")] [Range(0f, 4f)] [SerializeField] private float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, target.position.z + offset.z);
    }
}