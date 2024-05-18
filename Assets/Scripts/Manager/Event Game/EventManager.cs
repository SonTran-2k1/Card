using System;
using Controller;
public class EventManager : RingSingleton<EventManager>
{
    public Action OnWinGame;

    public void Action_WinGame()
    {
        OnWinGame?.Invoke();
    }
}
