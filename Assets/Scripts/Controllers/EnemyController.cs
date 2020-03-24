using UnityEngine;

/// <summary>
/// Class to hold enemy movement.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float speed = 1f;
    public bool canMove = true;

    [Header("Path settings")]
    [SerializeField] private EnemyPath pathToFollow;

    private Vector2 newPos;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        newPos = pathToFollow.GetStartPosition();
        newPos.y = transform.position.y;
    }

    private void Update()
    {
        startPos = pathToFollow.GetStartPosition();
        startPos.y = transform.position.y;

        endPos = pathToFollow.GetEndPosition();
        endPos.y = transform.position.y;

        if(transform.position.x == startPos.x)
        {
            newPos = endPos;
        }
        else if (transform.position.x == endPos.x)
        {
            newPos = startPos;
        }

        if (canMove)
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);

        SetDirection();
    }

    private void SetDirection ()
    {
        float angle = Mathf.Atan2(0, newPos == startPos ? -1 : 1) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
