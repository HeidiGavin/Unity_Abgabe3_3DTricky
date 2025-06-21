using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI textCounterDiamond;
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject panelWin;
    
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

    public void UpdateDiamondCount(int newDiamondCount)
    {
        textCounterDiamond.text = newDiamondCount.ToString();
    }

    public void ShowPanelLost()
    {
        panelLost.SetActive(true);
    }

    public void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }
    
}
