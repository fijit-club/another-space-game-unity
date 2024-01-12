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

    public List<AudioClip> tauntSoundsHappy;
    public List<AudioClip> tauntSoundsAngry;
    public AudioClip startAudio;
    public AudioClip endAudio;
    public AudioClip dieAudio;

    public AudioSource audioSource;
    public AudioSource audioSourceBG;


    public Animator tauntAnimator;

    public static TauntController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSourceBG.Play();
    }

    public void PlayAudio(AudioClip currentClip)
    {
        audioSource.PlayOneShot(currentClip);
    }

     void PlayTauntHappy()
    {
        audioSource.PlayOneShot(tauntSoundsHappy[Random.Range(0, tauntSoundsHappy.Count)]);

    }
     void PlayTauntAngry()
    {
        audioSource.PlayOneShot(tauntSoundsAngry[Random.Range(0, tauntSoundsAngry.Count)]);


    }


    public void Mute()
    {
        AudioListener.volume = 0;
    }

    public void UnMute()
    {
        AudioListener.volume = 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHappyTaunt()
    {
        happyImage.sprite = spritesHappy[Random.Range(0, spritesHappy.Count)];
        tauntAnimator.SetBool("HappyHawking", true);
        PlayTauntHappy();
        Invoke("DisableHappyTaunt", 0.1f);
        Debug.Log("tets end "); 
    }

    public void DisableHappyTaunt()
    {
        tauntAnimator.SetBool("HappyHawking", false) ;

    }
    
    public void ShowAngryTaunt()
    {
        angryImage.sprite = spritesAngry[Random.Range(0, spritesAngry.Count)];
        tauntAnimator.SetBool("AngryCop", true);
        PlayTauntAngry();
        Invoke("DisableAngryTaunt", 0.75f);
    }

    public void DisableAngryTaunt()
    {
        tauntAnimator.SetBool("AngryCop", false) ;
        audioSource.Stop();
    } 
    
    public void ShowEndTaunt()
    {
        angryImage.sprite = spritesAngry[Random.Range(0, spritesAngry.Count)];
        tauntAnimator.SetBool("GameOver", true);
        PlayAudio(dieAudio);
        Invoke("DisableEndTaunt", 0.5f);
    }

    public void DisableEndTaunt()
    {
        PlayAudio(endAudio);
        tauntAnimator.SetBool("GameOver", false) ;

    }
    
    public void ShowStartAnimation()
    {
        //angryImage.sprite = spritesAngry[Random.Range(0, spritesAngry.Count)];
        tauntAnimator.SetBool("StartAnim", true);
        PlayAudio(startAudio);
        Invoke("DisableStartTaunt", 0.5f);
    }

    public void DisableStartTaunt()
    {
        tauntAnimator.SetBool("StartAnim", false) ;

    }

    public void PlayStartSound()
    {
        PlayAudio(startAudio);
    }
}
