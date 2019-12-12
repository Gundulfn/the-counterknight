using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Person
{
   public GameObject x;
   public GameObject y;

   public override void Die()
   {
       Destroy(gameObject);
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
