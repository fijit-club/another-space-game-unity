using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public bool cantDie;
    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * 10f);
    }
}
