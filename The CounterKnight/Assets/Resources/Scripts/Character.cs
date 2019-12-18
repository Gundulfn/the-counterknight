using UnityEngine.SceneManagement;
using UnityEngine;

public class Character : Person
{
   public GameObject x;
   public GameObject y;

   public override void Die()
   {
       Destroy(gameObject);
       Score.saveBestScore();
       SceneManager.LoadScene(0, LoadSceneMode.Single);
   }

   public void openLeftBlock()
   {
       x.SetActive(true);
       y.SetActive(false);
   }

   public void openRightBlock()
   {
       y.SetActive(true);
       x.SetActive(false);
   }
}
