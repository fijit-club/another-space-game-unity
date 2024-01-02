using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public bool cantDie;
    public bool reversed;
    public Renderer coinRenderer;

    [SerializeField] private float incrementSpeed;
    [SerializeField] private float maxSpeed;
    
    private float _rotationSpeed;

    private void Start()
    {
        if (GameplayHandler.PlanetRotationSpeed < maxSpeed)
            GameplayHandler.PlanetRotationSpeed += incrementSpeed;

        _rotationSpeed = GameplayHandler.PlanetRotationSpeed;
        
        if (reversed) _rotationSpeed *= -1;
        coinRenderer.transform.parent = null;
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime * 10f);
    }
}
