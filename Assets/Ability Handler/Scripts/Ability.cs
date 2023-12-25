using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator abilityButton;

    public abstract void TriggerAbility();

    public abstract void DisableAbility();
}
