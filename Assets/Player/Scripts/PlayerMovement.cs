using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool stopMoving;
    
    [SerializeField] private float speed;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void LeavePlanet()
    {
        if (transform.parent == null) return;
        var transform1 = transform;
        
        Vector3 difference = transform1.parent.position - transform1.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform1.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);
        
        transform1.parent = null;

        stopMoving = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (stopMoving)
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        _rb.velocity = transform.up * speed * Time.fixedDeltaTime * 10f;
    }
}
