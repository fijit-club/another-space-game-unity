using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator abilityButton;
    [SerializeField] private float abilityTime;
    [SerializeField] private TMP_Text abilityNameText;
    [SerializeField] private string abilityName;
    
    protected IEnumerator DisableCounter()
    {
        abilityNameText.gameObject.SetActive(true);
        abilityNameText.text = abilityName;
        yield return new WaitForSeconds(abilityTime);
        DisableAbility();
        abilityNameText.gameObject.SetActive(false);
    }
    
    public abstract void TriggerAbility();

    public abstract void DisableAbility();
}
