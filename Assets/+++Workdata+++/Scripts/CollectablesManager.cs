using UnityEngine;
using UnityEngine.Rendering;

public class Collecatblesmanager : MonoBehaviour
{
    public static Collecatblesmanager instance;
    public int diamonds = 0;
    public int coins = 0;
    public int enemyBonus = 0;
    private int GetCurrentScore()
    {
        return coins * 100 + diamonds * 200 + enemyBonus;
    }
    
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
        int score = coins * 100 + diamonds * 200;
        uIManager.UpdateDiamondCount(diamonds);
        uIManager.UpdateCurrentScore(GetCurrentScore());
    } 
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
        uIManager.UpdateCoinCount(coins);
        int score = coins * 100 + diamonds * 200;
        uIManager.UpdateCurrentScore(GetCurrentScore());
    }

    public void AddEnemyBonus(int amount)
    {
        enemyBonus += amount;
        uIManager.UpdateCurrentScore(GetCurrentScore());
    }
}
