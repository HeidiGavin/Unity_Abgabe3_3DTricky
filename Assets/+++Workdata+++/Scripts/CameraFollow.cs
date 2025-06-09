using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followPlayer;
    public Vector2 followOffset;
    public float speed = 5f;
    private Vector2 threshold;
    private Rigidbody2D rb;
    void Start()
    {
        threshold = calculateThreshold();
        rb = followPlayer.GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        Vector2 follow = followPlayer.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
        
        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        float moveSpeed = rb.linearVelocity.magnitude > speed ? rb.linearVelocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
