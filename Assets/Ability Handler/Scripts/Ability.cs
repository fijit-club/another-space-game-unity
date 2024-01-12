using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected Animator abilityButton;
    [SerializeField] private float abilityTime;
    [SerializeField] private TMP_Text abilityNameText;
    [SerializeField] private string abilityName;
    [SerializeField] private Image abilityImage;
    [SerializeField] private Sprite abilitySprite;
    [SerializeField] private PlayerTrigger _playerTrigger;
    
    protected IEnumerator DisableCounter()
    {
        _playerTrigger.abilityEnabled = true;
        abilityNameText.gameObject.SetActive(true);
        abilityNameText.text = abilityName;
        abilityImage.sprite = abilitySprite;
        yield return new WaitForSeconds(abilityTime);
        DisableAbility();
        _playerTrigger.DisabledAbility();
        _playerTrigger.abilityEnabled = false;
        abilityNameText.gameObject.SetActive(false);
    }
    
    public abstract void TriggerAbility();

    public abstract void DisableAbility();
}
