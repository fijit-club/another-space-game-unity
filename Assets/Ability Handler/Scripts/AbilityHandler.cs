using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private SelectShip ship;
    [SerializeField] private Animator buttonAnimator;
    
    public void Trigger()
    {
        buttonAnimator.Play("Button", -1, 0f);
        ship.currentSpaceship.ability.TriggerAbility();
    }

    public void DisableAbility()
    {
        ship.currentSpaceship.ability.DisableAbility();
    }
}
