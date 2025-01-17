using System;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TimerNetworked time;

    private void Awake()
    {
        UpdateTimer(0f, time.TimeToDisplayNetworked.Value);
        time.TimeToDisplayNetworked.OnValueChanged += UpdateTimer;
    }

    public void UpdateTimer(float prevValue, float newValue)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(newValue);
        timerText.text = timeSpan.ToString(format:@"mm\:ss\:ff");
    }
}
