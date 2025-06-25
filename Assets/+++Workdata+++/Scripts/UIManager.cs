using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI textCounterCoins;
    [SerializeField] private TextMeshProUGUI textCounterDiamonds;
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreTextInGame;
    
    private int currentScore = 0;
    private int highScore = 0;
    
    private void Start()
    { 
        panelLost.SetActive(false);
        panelWin.SetActive(false);

        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.Save();
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    public void ReloadLevel()
    {
        ResetCurrentScore();
        
        PlayerPrefs.SetInt("Highscore", 0);
        PlayerPrefs.Save();
        
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

    public void UpdateDiamondCount(int newDiamondCount)
    {
        textCounterDiamonds.text = newDiamondCount.ToString();
    }

    public void UpdateCurrentScore(int score)
    {
        currentScore = score;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        
        UpdateHighScoreText();
    }
    
    private void UpdateHighScoreText()
    {
        if (currentScoreTextInGame.text != null)
        {
            currentScoreTextInGame.text = "" + currentScore;  
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        UpdateHighScoreText();
    }
    
    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void ShowPanelWinScore(int coins, int diamonds, int bonusPoints, float time)
    {
        panelWin.SetActive(true);
        int finalScore = coins * 100 + diamonds * 200 + bonusPoints - Mathf.RoundToInt(time * 5);
        if (finalScore < 0) finalScore = 0;

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        
        finalScoreText.text = "You Scored " + finalScore;
        UpdateHighScoreText();
    }
    
}
