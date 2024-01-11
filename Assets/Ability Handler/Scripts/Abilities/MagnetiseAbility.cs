using UnityEngine;

public class MagnetiseAbility : Ability
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float magnetStrength = .1f;
    
    public override void TriggerAbility()
    {
        playerMovement.magnetStrength = magnetStrength;
        playerMovement.magnetise = true;
        StartCoroutine(DisableCounter());
    }

    public override void DisableAbility()
    {
        playerMovement.magnetise = false;
    }
}
