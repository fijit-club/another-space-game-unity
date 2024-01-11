using UnityEngine;

public class CancelExplosion : Ability
{
    [SerializeField] private PlayerTrigger playerTrigger;
    
    public override void TriggerAbility()
    {
        playerTrigger.savePlanetAbility = true;
        var currentPlanet = playerTrigger.transform.parent;
        if (currentPlanet != null)
            currentPlanet.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(DisableCounter());
    }

    public override void DisableAbility()
    {
        playerTrigger.savePlanetAbility = false;
    }
}
