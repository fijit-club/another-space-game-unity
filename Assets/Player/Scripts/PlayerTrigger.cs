using System;
using System.Collections;
using System.Collections.Generic;
using SpaceEscape;
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
    public List<GameObject> planets = new List<GameObject>();
    public PlanetData planetData;
    public int planetsCrossed;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameOverState gameOverState;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private BasicMainMenuComponents mainMenuComponents;
    
    private GameObject _oldPlanet;
    private Transform _currentPlanet;
    private float _rotation = 180f;
    private float _maxInclusive;
    private Transform _nextPlanet;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Planet"))
        {
            if (!col.GetComponent<PlanetRotation>().cantDie)
                col.transform.GetChild(0).gameObject.SetActive(true);

            _currentPlanet = col.transform;
            
            if (planetsCrossed == 2)
            {
                Destroy(planets[0]);
                planets.RemoveAt(0);
                planetsCrossed = 0;
            }
            
            if (!col.GetComponent<PlanetRotation>().cantDie)
                planetsCrossed++;
            
            AttachToPlanet(col);
            var planetInst = SpawnPlanet();
            _nextPlanet = planetInst;
            playerMovement.nextPlanet = planetInst;
            planets.Add(planetInst.gameObject);
            
            UpdateCameraLocation(col.transform, planetInst.transform);
        }
        else if (col.CompareTag("DeadZone"))
        {
            GameOver();
        }
        else if (col.CompareTag("Coin"))
        {
            Vector3 difference = _nextPlanet.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
            Destroy(col.gameObject);
        }
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
        playerVisual.SetActive(false);
        GameStateManager.ChangeState(gameOverState);
    }

    private void AttachToPlanet(Collider2D col)
    {
        Vector3 difference = col.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        Transform playerTransform;
        (playerTransform = transform).localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + _rotation);

        playerTransform.parent = col.transform;
        playerMovement.stopMoving = true;

        col.GetComponent<Collider2D>().enabled = false;
    }

    private Transform SpawnPlanet()
    {
        planetData.startY += Random.Range(planetData.minIncrementY, planetData.maxIncrementY);

        var newPos = new Vector3(Random.Range(planetData.minX, planetData.maxX), planetData.startY, 0f);
        var newPlanetInstance = Instantiate(planetData.planet, newPos, Quaternion.identity);

        int r = Random.Range(0, 2);
        if (r == 1)
        {
            _rotation = 0f;
            newPlanetInstance.GetComponent<PlanetRotation>().reversed = true;
        }
        else
        {
            _rotation = 180f;
        }

        mainMenuComponents.planets.Add(newPlanetInstance);
        
        if (Random.Range(0, 3) == 1 && !_currentPlanet.GetComponent<PlanetRotation>().cantDie)
            CoinLocation(newPlanetInstance.transform);
        
        return newPlanetInstance.transform;
    }

    private void CoinLocation(Transform planet)
    {
        var coinGameObject = _currentPlanet.GetComponent<PlanetRotation>().coinRenderer.gameObject;
        coinGameObject.SetActive(true);
        
        Vector3 difference = planet.position - coinGameObject.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        coinGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        _maxInclusive = Vector3.Distance(planet.position, _currentPlanet.position) - 1f;
        coinGameObject.transform.Translate(new Vector3(Random.Range(1f, _maxInclusive), 0f, 0f));

        var pos = coinGameObject.transform.position;
        int range = Random.Range(0, 2);
        if (range == 0)
            pos.x = Random.Range(-.6f, -.3f);
        else
            pos.x = Random.Range(.3f, .6f);
        pos.x += _currentPlanet.position.x;
        coinGameObject.transform.position = pos;
        
        mainMenuComponents.planets.Add(coinGameObject);
    }

    private void UpdateCameraLocation(Transform currentPlanet, Transform nextPlanet)
    {
        var newPos = (currentPlanet.position + nextPlanet.position) / 2;
        cameraTransform.position = new Vector3(newPos.x, newPos.y, -10f);
    }
}
