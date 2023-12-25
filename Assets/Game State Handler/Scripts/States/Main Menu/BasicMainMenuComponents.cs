using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainMenuState))]
public class BasicMainMenuComponents : InGameComponents
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private MonoBehaviour[] componentsToEnable;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private Collider2D firstPlanet;
    [SerializeField] private PlayerTrigger playerTrigger;
    
    public List<GameObject> planets;
    
    private Vector3 _oldPlayerPosition;
    private Vector3 _oldCameraPosition;

    private void Awake()
    {
        _oldPlayerPosition = player.position;
        _oldCameraPosition = cameraTransform.position;
    }

    public override void EnteredState()
    {
        foreach (var planet in planets)
            Destroy(planet);

        planets.Clear();
        playerTrigger.planetData.startY = 0f;
        
        firstPlanet.enabled = true;
        player.position = _oldPlayerPosition;
        playerVisual.transform.position = _oldPlayerPosition;
        player.rotation = Quaternion.identity;
        cameraTransform.position = _oldCameraPosition;
        playerMovement.stopMoving = false;
        playerMovement.enabled = false;
        playerMovement.gameObject.SetActive(true);
        playerVisual.SetActive(true);
        cameraAnim.Play("ResetSize", -1, 0f);
        
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