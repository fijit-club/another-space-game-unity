using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int abilityIndex;
    
    [SerializeField] private Sprite[] sprites;
    
    private void Start()
    {
        if (FindObjectOfType<PlayerTrigger>().abilityEnabled) gameObject.SetActive(false);
        int r = Random.Range(0, sprites.Length);
        abilityIndex = r;
        GetComponent<SpriteRenderer>().sprite = sprites[r];
    }
}
