using UnityEngine;

public enum GameState
{
    Gameplay,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameState _currentState = GameState.Gameplay;
    private bool _isPaused = false;


    private void Update()
    {
        switch (_currentState)
        {
            case GameState.Gameplay:
                UpdateGameplay();
                break;
            case GameState.Pause:
                UpdatePause();
                break;
            case GameState.GameOver:
                UpdateGameOver();
                break;
        }
    }

    private void UpdateGameplay()
    {
        Time.timeScale = 1f;
        IsDead();
        CheckForPause();
    }

    private void UpdatePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            GameEvents.TriggerGamePauseStateChanged(_isPaused);
        }
        if (!_isPaused)
        {
            GameEvents.TriggerGamePauseStateChanged(false);
            ChangeState(GameState.Gameplay);
        }
    }

    private void UpdateGameOver()
    {
            _gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        
    }



    private void IsDead()
    {
        if (_player.IsDead)
        {
            ChangeState(GameState.GameOver);
        }
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            if (_currentState == GameState.Gameplay)
            {
                ChangeState(GameState.Pause);
            }
            else if (_currentState == GameState.Pause)
            {
                ChangeState(GameState.Gameplay);
            }
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        _pauseScreen.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    public void ChangeState(GameState newState)
    {
        _currentState = newState;
    }
}
