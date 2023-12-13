using UnityEngine;

[RequireComponent(typeof(GameOverState))]
public class BasicGameOverStateComponents : InGameComponents
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;    
    
    public override void EnteredState()
    {
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
        foreach (var go in gameObjectsToEnable)
            go.SetActive(false);
        
        foreach (var component in componentsToEnable)
            component.enabled = false;
    }
}
