using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState1
{
    ENEMY_ATTACK,
    ENEMY_WALKING,
    ENEMY_DEATH,
    ENEMY_IDLE
}


public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected float
    m_health = 5,
    m_speed = 5;

    private Vector2 playerTrans;

    private Animator enemy_Anim;

    private Animation enemy_Walking;
    private Animation enemy_Attacking;
    private Animation enemy_Hit;
    private Animation enemy_Death;
    private Animation enemy_Idle;

    private bool inRange = false;


    void Start ()
    {
        
        enemy_Anim = GameObject.Find("Melee_Enemy").GetComponent<Animator>();
        playerTrans = GameObject.Find("Player").transform.position;

        enemy_Attacking = Resources.Load("Animations/EnemyAni/Melee_Enemy_Attack") as Animation;

    }

    EnemyState1 CurrentState = EnemyState1.ENEMY_WALKING;

	void Update ()
    {

        if (inRange == false)
        {
           
           transform.position = Vector2.MoveTowards(this.transform.position, playerTrans, 1.5f * Time.deltaTime);
        }

        switch (CurrentState)
        {
            case EnemyState1.ENEMY_WALKING:
                enemy_Anim.SetBool("Melee_Enemy_Attack", false);
                enemy_Anim.SetBool("Melee_Enemy_Idle", false);
                StopAllCoroutines();

                break;

            case EnemyState1.ENEMY_IDLE:
                break;

            case EnemyState1.ENEMY_ATTACK:
                break;

            case EnemyState1.ENEMY_DEATH:
                break;              


        }
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;
            StartCoroutine(enemyAttacking());
            
        }

    }

    void EnemyStateChange (EnemyState1 stateChange)
    {
        CurrentState = stateChange;
    }

    public void _Attack()
    {
        EnemyStateChange(EnemyState1.ENEMY_ATTACK);
    }

    public void _Idle()
    {
        EnemyStateChange(EnemyState1.ENEMY_IDLE);
    }

    IEnumerator enemyAttacking ()
    {
        enemy_Anim.SetBool("Melee_Enemy_Attack", true);

        yield return new WaitForSeconds(0.25f);

        enemy_Anim.SetBool("Melee_Enemy_Attack", false);

        StartCoroutine(enemyIdling());

    }

    IEnumerator enemyIdling ()
    {
        enemy_Anim.SetBool("Melee_Enemy_Idle", true);

        yield return new WaitForSeconds(1.5f);

        if (inRange = true)
        {
            StartCoroutine(enemyAttacking());
        }

        else
        {

        }
          
    }
    

    
}
