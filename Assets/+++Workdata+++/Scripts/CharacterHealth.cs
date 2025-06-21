using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int health;
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
      health -= damage;
      if (health <= 0)
      {
          Destroy(gameObject);
      }
    }
}
