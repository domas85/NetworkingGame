using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public TimerNetworked time;

    private void Awake()
    {
        UpdateTimer(0f, time.Time.Value);
        time.Time.OnValueChanged += UpdateTimer;
    }

    public void UpdateTimer(float prevValue, float newValue)
    {
        timerText.text = newValue.ToString();
    }
}
