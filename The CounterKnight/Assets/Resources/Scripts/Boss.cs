using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Person
{
   public override void Die()
   {
       Debug.Log("HEYEYEYE");
       reduceHp();
   }
}
