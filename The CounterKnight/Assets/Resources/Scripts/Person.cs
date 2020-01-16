using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private const int DEFAULT_HP = 4;
    private int hp = DEFAULT_HP;

    protected void setHp(int newHp)
    {
        hp = newHp;
    }

    public int getHp()
    {
        return hp;
    }

    internal void resetHp(int key) // For safety, research it
    {
        if(key == 132426233)
        {
            hp = DEFAULT_HP;
        }
    }

    protected void reduceHp()
    {
        hp--;

        // Check if hp is equal to or lower than zero
        if(hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
       Debug.Log("So you are just a person...");
    }
}
