using UnityEngine;
using UnityEngine.UI;

public class TriggerButtonAbility : MonoBehaviour
{
    [SerializeField] private Ability[] abilities;
    [SerializeField] private GameObject coin;
    [SerializeField] private Button[] buttons;
    
    public void Trigger(int id)
    {
        abilities[id].TriggerAbility();
        
        var collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        
        foreach (var collectible in collectibles)
        {
            Instantiate(coin, collectible.transform.position, collectible.transform.rotation);
            Destroy(collectible);
        }
    }
}
