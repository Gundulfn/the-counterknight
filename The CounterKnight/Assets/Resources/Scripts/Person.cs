using UnityEngine;

public class Person : MonoBehaviour
{
    private const int CHAR_HP = 4;
    private const int BOSS_DARKELF_HP = 20;
    private int defaultHp;
    private int hp;

    void Awake()
    {
        if(GetComponent<Boss>())
        {
            hp = BOSS_DARKELF_HP;
            defaultHp = BOSS_DARKELF_HP;
        }
        else
        {
            hp = CHAR_HP;
            defaultHp = CHAR_HP;
        }
    }

    public int getHp()
    {
        return hp;
    }

    internal void resetHp(int key) // For safety, research it
    {
        if(key == 132426233)
        {
            hp = defaultHp;
        }
    }

    protected void reduceHp(int damage = 1)
    {
        if(hp - damage <= 0)
        {
            hp = 0;
            Die();
        }
        else
        {
            hp -= damage;
        }
    }

    protected virtual void Die(){  }
}