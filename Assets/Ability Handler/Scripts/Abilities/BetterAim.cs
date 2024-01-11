using UnityEngine;

public class BetterAim : Ability
{
    [SerializeField] private GameObject shorterAim;
    [SerializeField] private GameObject longerAim;

    public override void TriggerAbility()
    {
        shorterAim.SetActive(false);
        longerAim.SetActive(true);
        StartCoroutine(DisableCounter());
    }

    public override void DisableAbility()
    {
        shorterAim.SetActive(true);
        longerAim.SetActive(false);
    }
}
