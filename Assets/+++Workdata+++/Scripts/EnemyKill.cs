using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private GameObject enemy;

    void Start()
    {
        enemy = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            Destroy(enemy);
            
        }
    }
}
