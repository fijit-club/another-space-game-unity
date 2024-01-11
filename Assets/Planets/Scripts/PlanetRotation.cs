using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public bool cantDie;
    public bool reversed;
    public Renderer coinRenderer;
    public Collectible collectible;
    
    [SerializeField] public CheckToxicity checkToxicity;
    [SerializeField] private float incrementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Sprite[] islandImages;
    [SerializeField] private SpriteRenderer islandSpriteRenderer;
    
    [SerializeField]
    public float _rotationSpeed;

    private GameObject _islandSprite;
    
    private void Start()
    {
        coinRenderer.transform.parent = null;
        collectible.transform.parent = null;
        islandSpriteRenderer.sprite = islandImages[Random.Range(0, islandImages.Length)];
        _islandSprite = islandSpriteRenderer.gameObject;
    }

    private void Update()
    {
        if (collectible)
            collectible.transform.eulerAngles = Vector3.zero;
        if (coinRenderer)
            coinRenderer.transform.eulerAngles = Vector3.zero;
        islandSpriteRenderer.transform.parent = null;
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime * 10f);
    }

    private void OnDestroy()
    {
        Destroy(_islandSprite);
    }
}
