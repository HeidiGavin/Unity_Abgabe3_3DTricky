using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] TextMeshProUGUI timerText;
    
    void Update()
    {
        timerText.text = timer.ToString("0.00");
        timer += Time.deltaTime;
    }
}
