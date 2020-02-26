using System.Collections;
using UnityEngine;

public class Boss : Person
{
    private static int bossId = 0;
    private static GameObject currentBossObj;
    
    private Animator animator;
    private Animation anim;

    private AudioSource aud;
    private SpriteRenderer spriteRend;
    private GameObject arrowPrefab;
    private Transform arrowPlacement;
    private Rigidbody2D rb;

    private const float MOVE_SPEED = -200;
    
    private const float FIRST_JUMP_DELAY = 2;
    private static float defaultJumpDelay = FIRST_JUMP_DELAY;
    private static float jumpDelay = defaultJumpDelay;
    
    private const float FIRST_ATTACK_DELAY = 3;
    private static float defaultAttackDelay = FIRST_ATTACK_DELAY;
    private static float attackDelay = defaultAttackDelay;
    
    private bool isRage = false;
    
    private int defaultBossHp;
    private Vector3 firstPos;

    void Start()
    {
        currentBossObj = gameObject;
        defaultBossHp = getHp();

        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        
        rb = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
        spriteRend = GetComponent<SpriteRenderer>();
        arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow(Boss_DE)");
        arrowPlacement = transform.GetChild(0);

        StartCoroutine(bossEvent());
    }
    
    void Update()
    {
        if(animator.enabled)
        {
            if(transform.position.x <= 0)
            {
                spriteRend.flipX = false;
            }
            else
            {
                spriteRend.flipX = true;
            }
            
            if(attackDelay <= 0)
            {
                animator.SetTrigger("attack");
                attackDelay = defaultAttackDelay;
            }

            if(jumpDelay <= 0) // Changeable according to boss
            {
                if(transform.position.x <= 0)
                {
                    animator.SetTrigger("jumpNormal");
                    animator.ResetTrigger("jumpReverse");
                }
                else
                {
                    animator.SetTrigger("jumpReverse");
                    animator.ResetTrigger("jumpNormal");
                }
                
                jumpDelay = defaultJumpDelay;    
            }
            
            attackDelay -= Time.deltaTime;
            jumpDelay -= Time.deltaTime; // Changeable according to boss
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Lighting>())
        {
            reduceBossHp();
        }
    }

    public void reduceBossHp( int damage = 1) // Changeable according to boss
    {
        if(animator.enabled)
        {
            reduceHp(damage);

            if(UIHandler.soundOn && getHp() > 0)
            {
                // Rage moment
                if(getHp() <= defaultBossHp / 2 && !isRage)
                {
                    aud.clip = (AudioClip)Resources.Load("Audios/Sounds/Boss(DE)_Angry");
                    aud.Play();

                    isRage = true;
                    defaultAttackDelay = 1f;
                }
                else
                {
                    aud.clip = (AudioClip)Resources.Load("Audios/Sounds/Boss(DE)_Hit");
                    aud.Play();
                }            
            }

            if(defaultJumpDelay > 0.8f)
            {
                defaultJumpDelay -= 0.2f; 
            }
        }
    }

    public static void reduceBossHpWithSkill(int skillDamage)
    {
        if(currentBossObj)
        {
            switch(bossId)
            {
            case 0:
                currentBossObj.GetComponent<Boss>().reduceBossHp(skillDamage);
                break;
            default:
                break;    
            }
        }
    }

    protected override void Die()
    {
        animator.SetBool("isDead", true);

        if(UIHandler.soundOn)
        {
            aud.clip = (AudioClip)Resources.Load("Audios/Sounds/Boss(DE)_Dead");
            aud.Play();
        }
    }

    private void shotArrow() // Changeable according to boss
    {
        if(transform.position.x <= 0)
        {
            arrowPlacement.localPosition = new Vector3(Mathf.Abs(arrowPlacement.localPosition.x), 0, 0);
        }
        else
        {
            arrowPlacement.localPosition = new Vector3(-Mathf.Abs(arrowPlacement.localPosition.x), 0, 0);
        }  

        GameObject arrow = Instantiate(arrowPrefab, arrowPlacement.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().setBossTransform(transform);
    }

    private IEnumerator bossEvent()
    {
        yield return new WaitForSeconds(2); // Boss incoming delay
        
        animator.enabled = false;
        rb.velocity = transform.up * MOVE_SPEED * Time.deltaTime;

        UIHandler.stopMusicAud();
        
        yield return new WaitForSeconds(1);

        rb.velocity = 0 * transform.up;
        Feet.instance.stopAnimation();
        Background.setBackgroundActive();

        yield return new WaitForSeconds(0.5f);

        if(UIHandler.soundOn)
        {
            aud.Play();
        }

        yield return new WaitForSeconds(aud.clip.length);
    
        animator.enabled = true;
        firstPos = transform.position;

        UIHandler.changeMusicClip();
    }

    public void correctPosition()
    {
        if(transform.position.x <= 0)
        {
            firstPos = new Vector3(-Mathf.Abs(firstPos.x), firstPos.y, firstPos.z);
        }
        else
        {
            firstPos = new Vector3(Mathf.Abs(firstPos.x), firstPos.y, firstPos.z);
        }

        transform.position = firstPos;
    }

    void destroyBoss()
    {
        BossSpawnPoint.continueWave();
        Background.increaseSpeed();
        Enemy.buffEnemy();
        EnemySpawnPoint.buffSpawnPoint();

        Destroy(gameObject);
    }

    public static void resetBoss()
    {
        defaultJumpDelay = FIRST_JUMP_DELAY;
        jumpDelay = defaultJumpDelay;
        
        defaultAttackDelay = FIRST_ATTACK_DELAY;
        attackDelay = defaultAttackDelay;

        Destroy(currentBossObj);
    }
}