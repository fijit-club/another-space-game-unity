using UnityEngine;

public class MainMenuState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] mainMenuComponents;
    
    public void OnEnter()
    {
        foreach (var mainMenuComponent in mainMenuComponents)
            mainMenuComponent.EnteredState();
    }

    public void StateUpdate()
    {
        
    }

    public void OnExit()
    {
        foreach (var mainMenuComponent in mainMenuComponents)
            mainMenuComponent.LeftState();
    }
}