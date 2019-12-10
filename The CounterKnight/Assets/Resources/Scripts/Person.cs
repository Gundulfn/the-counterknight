using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private int hp = 5;

    void Update()
    {
        if(hp == 0)
        {
            Die();
        }
    }

    public int getHp()
    {
        return hp;
    }

    public void reduceHp()
    {
        hp--;
    }

    public virtual void Die()
    {
       Debug.Log("So you are just a person...");
    }
}
