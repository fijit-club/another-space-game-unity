using UnityEngine;

public class GameOverState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] gameOverStateComponents;

    public void OnEnter()
    {
        foreach (var gameOverStateComponent in gameOverStateComponents)
            gameOverStateComponent.EnteredState();
    }

    public void StateUpdate()
    {
    }

    public void OnExit()
    {
        foreach (var gameOverStateComponent in gameOverStateComponents)
            gameOverStateComponent.LeftState();
    }
}
