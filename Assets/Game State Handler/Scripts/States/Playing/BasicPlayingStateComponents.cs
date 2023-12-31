using UnityEngine;

[RequireComponent(typeof(PlayingState))]
public class BasicPlayingStateComponents : InGameComponents
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private GameObject inGameUI;
    
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private Animator abilityButton;
    [SerializeField] private PlayerTrigger playerTrigger;
    
    public override void EnteredState()
    {
        playerTrigger.planets.Clear();
        playerTrigger.planetsCrossed = 0;
        cameraController.CameraStart();
        inGameUI.SetActive(true);
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
