using System.Collections;
using UnityEngine;

public class Character : Person
{
    public static Character instance;

    private static Animator animator;
    private static AudioSource aud;
    
    private bool animatorActivity = true;
    
    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    public void reduceCharHp(int damage = 1)
    {
        reduceHp(damage);
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

    public void setAnimatorActivity()
    {
        animatorActivity = !animatorActivity;
        animator.enabled = animatorActivity;
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

    public static void triggerRightBlock()
    {
       animator.SetTrigger("rightBlock");
    }
}