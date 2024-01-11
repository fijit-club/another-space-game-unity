using System;
using System.Collections;
using System.Collections.Generic;
using SpaceEscape;
using TMPro;
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
    public float yFactor = 1f;
    public float yFactorIncrement = .01f;
    public float yFactorMax = 1.4f;
    public float startY = 0f;
}

public class PlayerTrigger : MonoBehaviour
{
    public List<GameObject> planets = new List<GameObject>();
    public PlanetData planetData;
    public int planetsCrossed;
    public bool savePlanetAbility;
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameOverState gameOverState;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private BasicMainMenuComponents mainMenuComponents;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private Transform directionVisual;
    [SerializeField] private MainMenuState mainMenuState;
    [SerializeField] private float maxExplosionSpeed;
    [SerializeField] private float incrementExplosionSpeed;
    [SerializeField] private float planetRotationStart;
    [SerializeField] private float planetRotationIncrement;
    [SerializeField] private float planetRotationMax;
    [SerializeField] private AudioSource landPlanet;
    [SerializeField] private Ability[] abilities;
    [SerializeField] private Animator redBorder;
    
    private GameObject _oldPlanet;
    private Transform _currentPlanet;
    private float _rotation = 180f;
    private float _maxInclusive;
    private Transform _nextPlanet;
    private int _coins;
    private float lastX;
    private static readonly int Factor = Animator.StringToHash("FACTOR");

    public void ResetCoins()
    {
        planetRotationStart = 15f;
        planetData.yFactor = 1f;
        coinsText.text = "0";
        _coins = 0;
        _rotation = 180f;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Planet"))
        {
            redBorder.gameObject.SetActive(true);
            var planetRot = col.GetComponent<PlanetRotation>();
            var explosionSpeed = planetRot.checkToxicity.explosionSpeed;
            redBorder.SetFloat(Factor, explosionSpeed);
            
            if (!col.GetComponent<PlanetRotation>().cantDie && !savePlanetAbility)
                col.transform.GetChild(0).gameObject.SetActive(true);

            _currentPlanet = col.transform;
            
            directionVisual.gameObject.SetActive(true);
            
            landPlanet.Play();
            
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
            if (!playerMovement.stopMoving)
            {
                Vector3 difference = _nextPlanet.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
            }

            Destroy(col.gameObject);
            _coins++;
            coinsText.text = _coins.ToString();
        }
        else if (col.CompareTag("Collectible"))
        {
            if (!playerMovement.stopMoving)
            {
                Vector3 difference = _nextPlanet.position - transform.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
            }

            Destroy(col.gameObject);

            int abilityIndex = col.GetComponent<Collectible>().abilityIndex;
            abilities[abilityIndex].TriggerAbility();
        }
    }

    public void GameOver()
    {
        if (GameStateManager.CurrentState == mainMenuState) return;
        gameObject.SetActive(false);
        playerVisual.SetActive(false);
        Bridge.GetInstance().UpdateCoins(_coins);
        Bridge.GetInstance().SendScore(playerMovement.score);
        GameStateManager.ChangeState(gameOverState);
    }

    private void AttachToPlanet(Collider2D col)
    {
        Vector3 difference = col.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        Transform playerTransform;
        (playerTransform = transform).localRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + _rotation);

        var rot = directionVisual.localEulerAngles;
        if (Math.Abs(_rotation - 180f) < .1f)
            rot.z = -90f;
        else
            rot.z = 90f;
        directionVisual.localEulerAngles = rot;

        playerTransform.parent = col.transform;
        playerMovement.stopMoving = true;

        col.GetComponent<Collider2D>().enabled = false;
    }

    private Transform SpawnPlanet()
    {
        if (playerMovement.score > 2000)
            planetData.maxIncrementY = 5f;
        
        planetData.startY += Random.Range(planetData.minIncrementY, planetData.maxIncrementY);

        float planetDataYFactor = Random.Range(planetData.minX, planetData.maxX) * planetData.yFactor;
        var newPos = new Vector3(planetDataYFactor + lastX, planetData.startY, 0f);
        lastX = newPos.x;
        if (planetData.yFactor < planetData.yFactorMax)
            planetData.yFactor += planetData.yFactorIncrement;
        var newPlanetInstance = Instantiate(planetData.planet, newPos, Quaternion.identity);
        var planetRotationHandler = newPlanetInstance.GetComponent<PlanetRotation>();
        planetRotationHandler._rotationSpeed = planetRotationStart;
        if (planetRotationStart < planetRotationMax)
            planetRotationStart += planetRotationIncrement;
            
        if (planetRotationHandler.checkToxicity.explosionSpeed < maxExplosionSpeed)
            planetRotationHandler.checkToxicity.explosionSpeed += incrementExplosionSpeed;
        else
            planetRotationHandler.checkToxicity.explosionSpeed = maxExplosionSpeed;
        

        int r = Random.Range(0, 2);
        if (r == 1)
        {
            _rotation = 0f;
            planetRotationHandler.reversed = true;
        }
        else
        {
            _rotation = 180f;
        }
        if (planetRotationHandler.reversed) planetRotationHandler._rotationSpeed *= -1;

        mainMenuComponents.planets.Add(newPlanetInstance);

        if (Random.Range(0, 3) == 1 && !_currentPlanet.GetComponent<PlanetRotation>().cantDie)
        {
            int i = Random.Range(0, 5);
            if (i == 1)
                CoinLocation(newPlanetInstance.transform, i);
            else
                CoinLocation(newPlanetInstance.transform, i);
        }
        
        return newPlanetInstance.transform;
    }

    private void CoinLocation(Transform planet, int index)
    {
        GameObject coinGameObject;
        if (index == 1)
        {
            coinGameObject = _currentPlanet.GetComponent<PlanetRotation>().collectible.gameObject;

            coinGameObject.SetActive(true);
        }
        else
        {
            coinGameObject = _currentPlanet.GetComponent<PlanetRotation>().coinRenderer.gameObject;
            coinGameObject.SetActive(true);
        }

        Vector3 difference = planet.position - coinGameObject.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        coinGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        _maxInclusive = Vector3.Distance(planet.position, _currentPlanet.position) - 1f;
        coinGameObject.transform.Translate(new Vector3(Random.Range(2f, _maxInclusive), 0f, 0f));

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
