
using System;
public class GameEvents 
{
    public static event Action<bool> OnGamePauseStateChanged;
   
  
    public static void TriggerGamePauseStateChanged(bool isPaused)
    {
        OnGamePauseStateChanged?.Invoke(isPaused);
    }
}
