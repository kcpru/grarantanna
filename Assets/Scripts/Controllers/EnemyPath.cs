using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [Header("Do not modify path in game mode!")]
    [SerializeField] private Color pathColor;
    [SerializeField] private float pathLength;

    public Vector2 GetStartPosition() => new Vector2(transform.position.x - (pathLength / 2), transform.position.y);

    public Vector2 GetEndPosition() => new Vector2(transform.position.x + (pathLength / 2), transform.position.y);

    private void OnDrawGizmos()
    {
        Color startColor = Gizmos.color;
        Gizmos.color = pathColor;

        Vector3 startPos = new Vector3(transform.position.x - (pathLength / 2), transform.position.y, transform.position.z);
        Vector3 endPos = new Vector3(transform.position.x + (pathLength / 2), transform.position.y, transform.position.z);

        Gizmos.DrawLine(startPos, endPos);

        Gizmos.color = startColor;
    }
}
