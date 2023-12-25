using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameOverState))]
public class BasicGameOverStateComponents : InGameComponents
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private SelectShip ship;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator abilityButton;
    
        
    public override void EnteredState()
    {
        if (abilityButton.gameObject.activeInHierarchy)
            abilityButton.Play("Idle", -1, 0f);

        var currentSpaceshipAbility = ship.currentSpaceship.ability;
        if (currentSpaceshipAbility)
            currentSpaceshipAbility.DisableAbility();
        
        foreach (var go in gameObjectsToEnable)
            go.SetActive(true);
        
        foreach (var component in componentsToEnable)
            component.enabled = true;

        foreach (var go in gameObjectsToDisable)
            go.SetActive(false);

        foreach (var component in componentsToDisable)
            component.enabled = false;
    }

    public override void LeftState()
    {
        playerMovement.ResetScore();
        
        foreach (var go in gameObjectsToEnable)
            go.SetActive(false);
        
        foreach (var component in componentsToEnable)
            component.enabled = false;
    }
}
