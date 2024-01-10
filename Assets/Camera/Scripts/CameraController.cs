using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator camAnim;
    [SerializeField] private Transform cameraFollower;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraFollower.position, 10f * Time.deltaTime);
    }

    public void CameraStart()
    {
        camAnim.Play("SetSize", -1, 0f);
    }
}
