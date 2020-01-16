using System.Collections;
using UnityEngine;

public class Character : Person
{
    private static Animator animator;
    private static AudioSource aud;

    void Start()
    {
        animator = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    public void reduceCharHp()
    {
        reduceHp();
        HpStatus.setHpStatus(this);

        if(getHp() != 0 && UIHandler.soundOn)
        {
            aud.clip = (AudioClip)Resources.Load("Audios/Sounds/KnightHit");
            aud.Play();
        }
    }
    
    protected override void Die()
    {
        if(UIHandler.soundOn)
        {
            aud.clip = (AudioClip)Resources.Load("Audios/Sounds/KnightDead"); 
            aud.Play();
        }
        
        Score.saveBestScore();

        UIHandler uIHandler = GameObject.FindObjectOfType<UIHandler>();
        uIHandler.openDeadUI();
    }

    public void resetCharacter()
   {
       resetHp(132426233);
       HpStatus.setHpStatus(this);

       Animator animator = GetComponent<Animator>();
       
       // 2 is index of "KnightFeet" GameObject
       Animator subAnimator = transform.GetChild(2).GetComponent<Animator>(); 
       animator.Rebind();
       subAnimator.Rebind();
   }

    // Animator Parameter Functions
    public static void setLeftHold(bool isLeftHold)
    {
       animator.SetBool("leftBlockHold", isLeftHold);
    } 

    public static void triggerLeftBlock()
    {
       animator.SetTrigger("leftBlock");
    }  

    public static void triggerRighttBlock()
    {
       animator.SetTrigger("rightBlock");
    }

     
}
