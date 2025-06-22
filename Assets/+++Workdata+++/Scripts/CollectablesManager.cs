using UnityEngine;
using UnityEngine.Rendering;

public class Collecatblesmanager : MonoBehaviour
{
    public static Collecatblesmanager instance;
    public int diamonds = 0;
    public int coins = 0;
    
    [SerializeField] private UIManager uIManager;

    void Awake()
    {
        if (instance == null) instance = this; 
        else Destroy(gameObject);
    }
    
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        Debug.Log("Diamonds: " + diamonds);
        int score = coins * 10 + diamonds * 20;
        uIManager.UpdateCurrentScore(score);
    } 
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
        uIManager.UpdateCoinCount(coins);
        int score = coins * 10 + diamonds * 20;
        uIManager.UpdateCurrentScore(score);
    }
}
