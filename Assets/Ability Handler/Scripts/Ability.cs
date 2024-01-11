using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator abilityButton;
    [SerializeField] private float abilityTime;
    
    protected IEnumerator DisableCounter()
    {
        yield return new WaitForSeconds(abilityTime);
        DisableAbility();
    }
    
    public abstract void TriggerAbility();

    public abstract void DisableAbility();
}
