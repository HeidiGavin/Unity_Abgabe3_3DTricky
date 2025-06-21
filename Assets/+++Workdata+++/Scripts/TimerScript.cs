using System.Collections;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    float timer = 0.0f;
    private bool isRunning = false;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameObject countdownPanel;
    [SerializeField] private SimpleCharacterControl playerControl;

    void Start()
    {
        timerText.text = "0.00";
        StartCoroutine(CountdownStart());
        
    }
    
    void Update()
    {
        if (!isRunning) return;
        
        timer += Time.deltaTime;
        timerText.text = timer.ToString("0.00");
    }

    IEnumerator CountdownStart()
    {
        countdownText.gameObject.SetActive(true);
        if (countdownPanel != null)
        {
            countdownPanel.SetActive(true);
        }

        for (int i = 3; i >= 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        
        countdownText.gameObject.SetActive(false);
        if (countdownPanel != null)
        {
            countdownPanel.SetActive(false);
        }
        
        timer = 0.0f;
        isRunning = true;

        if (playerControl != null)
        {
            playerControl.SetCanMove(true);
        }
    }
    
    public void StopTimer()
    {
        isRunning = false;
    }

    public void RestartTimer()
    {
        timer = 0.0f;
        isRunning = true;
    }

    public bool IsTimerRunning()
    {
        return isRunning;
    }

    public float GetTimer()
    {
        return timer;
    }
}
