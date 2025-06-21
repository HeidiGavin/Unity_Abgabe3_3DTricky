using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI textCounterCoins;
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    private void Start()
    { 
        panelLost.SetActive(false);
        panelWin.SetActive(false);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void ToNextLevel()
    {
      //  SceneManager.LoadScene("JumpNRub_Level2");
    }

    public void UpdateCoinCount(int newCoinCount)
    {
        textCounterCoins.text = newCoinCount.ToString(); 
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void ShowPanelWinScore(int coins, int diamonds, float time)
    {
        panelWin.SetActive(true);
        int finalScore = coins * 10 + diamonds * 25 - Mathf.RoundToInt(time);
        if (finalScore < 0) finalScore = 0;
        finalScoreText.text = "You Scored: " + finalScore;
    }
    
}
