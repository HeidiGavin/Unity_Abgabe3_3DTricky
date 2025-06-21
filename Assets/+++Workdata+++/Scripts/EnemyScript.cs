using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform [] patrolPoints;
    public float speed = 2;
    public int patrolDestination;
    private bool isDead = false;

    private Rigidbody2D rb;
    private Animator _animator;
    [SerializeField] private UIManager uiManager;
        
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (isDead) return;

        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                patrolDestination = 1;
            }
        }
        
        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                patrolDestination = 0;
            }
        }
        
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            uiManager.ShowPanelLost();
            
        }
    }
}
