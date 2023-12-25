using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public bool cantDie;
    public bool reversed;
    public Renderer coinRenderer;

    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        if (reversed) rotationSpeed *= -1;
        coinRenderer.transform.parent = null;
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * 10f);
    }
}
