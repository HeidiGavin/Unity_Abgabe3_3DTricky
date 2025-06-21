using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform patrolPoint_1;
    public Transform patrolPoint_2;
    public float speed = 2;
    private Vector3 target;
    private bool isDead = false;

    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private UIManager uiManager;
        
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = patrolPoint_2.position;
    }
    
    void Update()
    {
        if (isDead) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = (target == patrolPoint_1.position) ? patrolPoint_2.position : patrolPoint_1.position;
            Flip();
        }
    }//U SUCK THIS DOESNT WORK >:(

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            uiManager.ShowPanelLost();
            
        }
    }
}
