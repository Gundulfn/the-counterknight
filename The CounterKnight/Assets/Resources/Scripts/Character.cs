using UnityEngine.SceneManagement;
using UnityEngine;

public class Character : Person
{
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
}
