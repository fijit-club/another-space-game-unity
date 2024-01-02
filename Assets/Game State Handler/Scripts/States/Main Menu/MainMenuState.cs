using UnityEngine;

public class MainMenuState : MonoBehaviour, IState
{
    [SerializeField] private InGameComponents[] mainMenuComponents;
    
    public void OnEnter()
    {
        GameplayHandler.ExplosionSpeed = 0.5f;
        GameplayHandler.PlanetRotationSpeed = 20f;
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