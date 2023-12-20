using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private Animator abilityButton;

    public abstract void TriggerAbility();

    public abstract void DisableAbility();
}
