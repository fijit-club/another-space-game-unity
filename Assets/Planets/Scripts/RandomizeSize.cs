using UnityEngine;

public class RandomizeSize : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    
    private void Start()
    {
        float scale = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * scale;
    }
}
