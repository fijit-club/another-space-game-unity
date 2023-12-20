using UnityEngine;

public class SlowTimeAbility : Ability
{
    [SerializeField] private string animationName;
    
    public override void TriggerAbility()
    {
        Time.timeScale = .3f;
    }

    public override void DisableAbility()
    {
        Time.timeScale = 1f;
    }
}
