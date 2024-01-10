using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public bool cantDie;
    public bool reversed;
    public Renderer coinRenderer;
    
    [SerializeField] public CheckToxicity checkToxicity;
    [SerializeField] private float incrementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Sprite[] islandImages;
    [SerializeField] private SpriteRenderer islandSpriteRenderer;
    
    [SerializeField]
    public float _rotationSpeed;

    private void Start()
    {
        coinRenderer.transform.parent = null;
        islandSpriteRenderer.sprite = islandImages[Random.Range(0, islandImages.Length)];
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime * 10f);
    }
}
