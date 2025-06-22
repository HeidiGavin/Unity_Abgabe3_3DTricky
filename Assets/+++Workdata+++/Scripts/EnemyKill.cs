using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    private GameObject enemy;
    private Collecatblesmanager collecatblesManager;

    void Start()
    {
        enemy = transform.parent.gameObject;
        collecatblesManager = Collecatblesmanager.instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (collecatblesManager != null)
            {
                collecatblesManager.AddCoins(15);
            }
            else
            {
                Debug.LogWarning("CollectablesManager is null on enemy kill");
            }
            Destroy(enemy);
        }
    }
}
