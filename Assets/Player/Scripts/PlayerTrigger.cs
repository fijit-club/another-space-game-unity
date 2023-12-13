using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PlanetData
{
    public GameObject planet;
    public float minX;
    public float maxX;
    public float minIncrementY;
    public float maxIncrementY;
    public float startY = 0f;
}

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    public PlanetData planetData;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameOverState gameOverState;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private BasicMainMenuComponents mainMenuComponents;
    
    private GameObject _oldPlanet;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Planet"))
        {
            if (_oldPlanet == null)
            {
                _oldPlanet = col.gameObject;
            }
            else
            {
                if (!_oldPlanet.GetComponent<PlanetRotation>().cantDie)
                    Destroy(_oldPlanet);
                _oldPlanet = null;
            }
            _oldPlanet = col.gameObject;
            AttachToPlanet(col);
            var planetInst = SpawnPlanet();
            UpdateCameraLocation(col.transform, planetInst.transform);
        }
        else if (col.CompareTag("DeadZone"))
        {
            gameObject.SetActive(false);
            playerVisual.SetActive(false);
            GameStateManager.ChangeState(gameOverState);
        }
    }

    private void AttachToPlanet(Collider2D col)
    {
        Vector3 difference = col.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        Transform playerTransform;
        (playerTransform = transform).localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 180f);

        playerTransform.parent = col.transform;
        playerMovement.stopMoving = true;

        col.GetComponent<Collider2D>().enabled = false;
    }

    private Transform SpawnPlanet()
    {
        planetData.startY += Random.Range(planetData.minIncrementY, planetData.maxIncrementY);

        var newPos = new Vector3(Random.Range(planetData.minX, planetData.maxX), planetData.startY, 0f);
        var newPlanetInstance = Instantiate(planetData.planet, newPos, Quaternion.identity);

        mainMenuComponents.planets.Add(newPlanetInstance);
        
        return newPlanetInstance.transform;
    }

    private void UpdateCameraLocation(Transform currentPlanet, Transform nextPlanet)
    {
        var newPos = (currentPlanet.position + nextPlanet.position) / 2;
        cameraTransform.position = new Vector3(newPos.x, newPos.y, -10f);
    }
}
