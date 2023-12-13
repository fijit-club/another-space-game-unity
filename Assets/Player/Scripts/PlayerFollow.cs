using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, 10f * Time.deltaTime);
    }
}
