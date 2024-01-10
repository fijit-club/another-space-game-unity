using UnityEngine;

public class CheckToxicity : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    
    private static readonly int ExplosionSpeed = Animator.StringToHash("ExplosionSpeed");

    public float explosionSpeed;
    
    private void Start()
    {
        var animator = GetComponent<Animator>();
        animator.SetFloat(ExplosionSpeed, explosionSpeed);
    }

    public void Explode()
    {
        if (transform.parent.childCount > 3)
        {
            var player = FindObjectOfType<PlayerTrigger>();
            player.transform.parent = null;
            player.GameOver();
        }

        var explosionInstance = Instantiate(explosion, transform.parent.position, Quaternion.identity);
        Destroy(explosionInstance, 2f);
        Destroy(transform.parent.gameObject);
    }
}
