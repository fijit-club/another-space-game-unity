using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool stopMoving;
    public bool magnetise;
    public float magnetStrength;
    public Transform nextPlanet;
    
    [SerializeField] private float speed;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private GameObject directionVisual;
    [SerializeField] private float speedIncrement;
    [SerializeField] private float maxSpeed;
    [SerializeField] private AudioSource leavePlanetAudio;

    private Rigidbody2D _rb;
    public int score;
    private float _time;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ResetScore()
    {
        scoreText.text = "0";
        score = 0;
        playerTrigger.ResetCoins();
        speed = 15;
    }

    public void LeavePlanet()
    {
        if (transform.parent == null) return;
        var transform1 = transform;

        if (speed < maxSpeed)
            speed += speedIncrement;
        else
            speed = maxSpeed;
        
        leavePlanetAudio.Play();
        
        directionVisual.SetActive(false);
        
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

    private void CalculateScore()
    {
        _time += Time.deltaTime;
        if (_time > .05f)
        {
            _time = 0f;
            score++;
            scoreText.text = score.ToString();
        }
    }
    
    private void FixedUpdate()
    {
        if (!stopMoving)
        {
            
            if (magnetise)
            {
                var transform1 = transform;
                Vector3 difference = nextPlanet.position - transform1.position;
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                
                var targetRotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90f);
                transform1.rotation = Quaternion.Lerp(transform1.rotation, targetRotation, magnetStrength);
            }
            CalculateScore();
        }
        else
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        _rb.velocity = transform.up * speed * Time.fixedDeltaTime * 10f;
    }
}
