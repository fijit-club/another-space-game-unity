using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
