using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float speed = 1f;

    [Header("Path settings")]
    [SerializeField] private EnemyPath path;

    private Vector2 newPos;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        newPos = path.GetStartPosition();
        newPos.y = transform.position.y;
    }

    private void Update()
    {
        startPos = path.GetStartPosition();
        startPos.y = transform.position.y;

        endPos = path.GetEndPosition();
        endPos.y = transform.position.y;

        if (transform.position.x == startPos.x)
        {
            newPos = endPos;
        }
        else if (transform.position.x == endPos.x)
        {
            newPos = startPos;
        }

        transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }
}
