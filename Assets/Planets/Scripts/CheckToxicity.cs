using UnityEngine;

public class CheckToxicity : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    
    private static readonly int ExplosionSpeed = Animator.StringToHash("ExplosionSpeed");

    private void Start()
    {
        var animator = GetComponent<Animator>();
        animator.SetFloat(ExplosionSpeed, GameplayHandler.ExplosionSpeed);
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
