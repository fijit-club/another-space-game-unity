using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TauntController : MonoBehaviour
{
    public List<Sprite> spritesAngry = new List<Sprite>();
    public List<Sprite> spritesHappy = new List<Sprite>();

    public Image happyImage;
    public Image angryImage;


    public Animator tauntAnimator;

    public static TauntController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHappyTaunt()
    {
        happyImage.sprite = spritesHappy[Random.Range(0, spritesHappy.Count)];
        tauntAnimator.SetBool("HappyShaq", true);
        //AudioController.Instance.PlayTauntHappy();
        Invoke("DisableHappyTaunt", 0.1f);
    }

    public void DisableHappyTaunt()
    {
        tauntAnimator.SetBool("HappyShaq", false) ;

    }
    
    public void ShowAngryTaunt()
    {
        angryImage.sprite = spritesAngry[Random.Range(0, spritesAngry.Count)];
        tauntAnimator.SetBool("AngryShaq", true);
        //AudioController.Instance.PlayTauntAngry();
        Invoke("DisableAngryTaunt", 0.1f);
    }

    public void DisableAngryTaunt()
    {
        tauntAnimator.SetBool("AngryShaq", false) ;

    }
}
