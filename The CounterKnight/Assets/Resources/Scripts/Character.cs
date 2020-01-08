using System.Collections;
using UnityEngine;

public class Character : Person
{
    private static Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void reduceCharHp()
    {
        reduceHp();
        HpStatus.setHpStatus(this);
    }
    
    protected override void Die()
    {
        // Play Dead Animation, after open DeadUI
        Score.saveBestScore();

        UIHandler uIHandler = GameObject.FindObjectOfType<UIHandler>();
        uIHandler.openDeadUI();
    }

    public void resetCharacter()
   {
       resetHp(132426233);
       HpStatus.setHpStatus(this);

       Animator animator = GetComponent<Animator>();
       animator.Rebind();
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
