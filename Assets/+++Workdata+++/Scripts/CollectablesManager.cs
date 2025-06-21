using UnityEngine;

public class Collecatblesmanager : MonoBehaviour
{
    public static Collecatblesmanager instance;
    public int diamonds = 0;
    public int coins = 0;
    
    [SerializeField]

    void Awake()
    {
        if (instance == null) instance = this; 
        else Destroy(gameObject);
    }
    
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        Debug.Log("Diamonds: " + diamonds);
    } 
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }
}
