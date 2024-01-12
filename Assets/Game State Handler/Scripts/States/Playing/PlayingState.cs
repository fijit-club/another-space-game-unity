using UnityEngine;

public class PlayingState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] playingStateComponents;
    
    public void OnEnter()
    {
        TauntController.instance.ShowStartAnimation();
        Invoke("StartGame", 3f);
    }

    void StartGame()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.EnteredState();

    }

    public void StateUpdate()
    {
    }

    public void OnExit()
    {
        foreach (var playingStateComponent in playingStateComponents)
            playingStateComponent.LeftState();
    }
}
