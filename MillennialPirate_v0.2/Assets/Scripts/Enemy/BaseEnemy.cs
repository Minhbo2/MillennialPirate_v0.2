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
    m_speed = 1.5f;

    [SerializeField] private Transform rootTransform;

    private Vector3 playerTrans;

    private Animator enemy_Anim;

    private Animation enemy_Walking;
    private Animation enemy_Attacking;
    private Animation enemy_Hit;
    private Animation enemy_Death;
    private Animation enemy_Idle;

    private bool inRange = false;


    void Start ()
    {
        
        enemy_Anim = GetComponent<Animator>();
        playerTrans = GameObject.Find("Player").transform.position;

        enemy_Attacking = Resources.Load("Animations/EnemyAni/Melee_Enemy_Attack") as Animation;

    }

    EnemyState1 CurrentState = EnemyState1.ENEMY_WALKING;

	void Update ()
    {

        if (inRange == false)
        {
           
           transform.position += (playerTrans - rootTransform.position).normalized * m_speed * Time.deltaTime;
          
        }

        switch (CurrentState)
        {
            case EnemyState1.ENEMY_WALKING:
         
                break;

            case EnemyState1.ENEMY_IDLE:
                enemy_Anim.SetBool("isIdle", true);
                break;

            case EnemyState1.ENEMY_ATTACK:
                enemy_Anim.SetBool("attackTrigger", true);
                break;

            case EnemyState1.ENEMY_DEATH:
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;              

        }

        if (m_health <= 0)
        {
            CurrentState = EnemyState1.ENEMY_DEATH;
            Destroy(gameObject);
        }
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;
            CurrentState = EnemyState1.ENEMY_ATTACK;
        }

        else
        {
            CurrentState = EnemyState1.ENEMY_IDLE;

        }


        //if(collision.tag == "LightAttack")
        //{
           //m_health -= 1;
        //}

        //else (collision.tag == "HeavyAttack")
        //{
           //m_health -= 3;
        //}

    }

    void EnemyStateChange (EnemyState1 stateChange)
    {
        CurrentState = stateChange;
    }   

    
}
