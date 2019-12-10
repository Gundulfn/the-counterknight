using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Person
{
   public override void Die()
   {
       Destroy(gameObject);
   }
}
