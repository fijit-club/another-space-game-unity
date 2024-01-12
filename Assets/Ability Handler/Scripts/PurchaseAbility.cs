using System.Collections.Generic;
using SpaceEscape;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseAbility : MonoBehaviour
{
    public List<int> purchasedAbilitiesIDs;
    
    [SerializeField] private Image[] abilityButtonsShop;
    [SerializeField] private Button[] abilityButtonsInGame;
    
    public void CheckAbilities()
    {
        for (int index = 0; index < purchasedAbilitiesIDs.Count; index++)
        {
            var purchasedAbilityID = purchasedAbilitiesIDs[index];
            abilityButtonsShop[purchasedAbilityID].transform.GetChild(0).gameObject.SetActive(false);
            abilityButtonsInGame[purchasedAbilityID].interactable = true;
        }
    }

    public void Purchase(int id)
    {
        if (!purchasedAbilitiesIDs.Contains(id))
            purchasedAbilitiesIDs.Add(id);

        abilityButtonsInGame[id].interactable = true;
        
        CheckAbilities();
    }
}
