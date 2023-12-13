using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private MainMenuState mainMenuState;
    
    public void RestartGame()
    {
        GameStateManager.ChangeState(mainMenuState);
    }
}
