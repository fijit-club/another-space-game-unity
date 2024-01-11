using UnityEngine;

public class SlowTimeAbility : Ability
{
    [SerializeField] private string animationName;
    [SerializeField] private float reducedTime;
    
    public override void TriggerAbility()
    {
        Time.timeScale = reducedTime;
        StartCoroutine(DisableCounter());
    }

    public override void DisableAbility()
    {
        Time.timeScale = 1f;
    }
}
