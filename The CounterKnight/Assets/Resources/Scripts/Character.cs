using UnityEngine.SceneManagement;
using UnityEngine;

public class Character : Person
{
   public void reduceCharHp()
   {
       reduceHp();
       HpStatus.setHpStatus(this);
   }
   
   public void resetAnimator()
   {
       Animator animator = GetComponent<Animator>();
       animator.Rebind();
   }

   protected override void Die()
   {
       Destroy(gameObject);
       Score.saveBestScore();
       Time.timeScale = 0;
   }
}
