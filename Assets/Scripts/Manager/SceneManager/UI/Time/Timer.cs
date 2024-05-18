using TMPro;
using UnityEngine;

public enum TimerState
{
    Start,
    End
}

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float currentTime;

    public TimerState timerState;

    private void Update()
    {
        if (timerState == TimerState.Start)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                // Thực hiện hành động khi hết thời gian ở đây
            {
                timerState = TimerState.End;
                UiManager.Instance.LoseGame();
                var minutes = Mathf.FloorToInt(0);
                var seconds = Mathf.FloorToInt(0);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                UpdateTimerDisplay();
            }
        }
    }

    private void OnEnable()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        currentTime = GameManager.Instance._gameController._listDataLevel[GameManager.Instance.LoadLevel() - 1]
            .time; // Sử dụng thời gian từ DataGame
        timerState = TimerState.Start;
    }

    private void UpdateTimerDisplay()
    {
        var minutes = Mathf.FloorToInt(currentTime / 60);
        var seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
