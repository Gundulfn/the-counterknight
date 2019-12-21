using UnityEngine.SceneManagement;
using UnityEngine;

public class Character : Person
{
   protected override void Die()
   {
       Destroy(gameObject);
       Score.saveBestScore();
       SceneManager.LoadScene(0, LoadSceneMode.Single);
   }
}
