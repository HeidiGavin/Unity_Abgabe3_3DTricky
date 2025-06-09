using UnityEngine;

public class Diamondmanager : MonoBehaviour
{
    public static Diamondmanager instance;
    public int diamonds = 0;

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
}
