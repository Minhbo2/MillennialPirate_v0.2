using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Melee_Enemy_01 : EnemyClass
{
  
    protected bool inRange = false;
    protected float yOffset = -1.9f;


    void Start ()
    {
        health = 3;
        ChangingDirection();
        StartCoroutine(MoveTowardTarget(player.position));
        CurrentState = EnemyState.ENEMY_WALKING;
    }



	public override void Update ()
    {
        Debug.Log(CurrentState);
        transform.position = new Vector2(transform.position.x, yOffset);

        switch (CurrentState)
        {
            case EnemyState.ENEMY_WALKING:
                enemy_Anim.SetBool("isHit", false);
                if (DistanceToTarget(player.position) <= 1.8f)
                {
                    StopAllCoroutines();
                    CurrentState = EnemyState.ENEMY_ATTACK;
                }
                break;

            case EnemyState.ENEMY_IDLE:
                StartCoroutine(IdleDelay());
                break;

            case EnemyState.ENEMY_ATTACK:
                enemy_Anim.SetBool("isHit", false);
                StartCoroutine(AttackDelay());
                break;

            case EnemyState.ENEMY_HIT:
                StopAllCoroutines();
                enemy_Anim.SetBool("attackTrigger", false);
                enemy_Anim.SetBool("isIdle", false);
                enemy_Anim.SetBool("isHit", true);
                
                break;

            case EnemyState.ENEMY_DEATH:
                StopAllCoroutines();
                enemy_Anim.SetBool("attackTrigger", false);
                enemy_Anim.SetBool("isHit", false);
                enemy_Anim.SetBool("isIdle", false);
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;              

        }

        if (health <= 0)
        {
            CurrentState = EnemyState.ENEMY_DEATH;
            //Destroy(gameObject);
        }
	}


    void DestroyThis()
    {
        Destroy(this.gameObject);
    }




    public void CheckState()
    {
        if (DistanceToTarget(player.position) >= 1.8f)
        {
            StartCoroutine(MoveTowardTarget(player.position));
            CurrentState = EnemyState.ENEMY_WALKING;
        }
    }




    protected IEnumerator AttackDelay ()
    {
        enemy_Anim.SetBool("attackTrigger", true);

        yield return new WaitForSeconds(1.20f);

        enemy_Anim.SetBool("attackTrigger", false);

        CurrentState = EnemyState.ENEMY_IDLE;

    }

    protected IEnumerator IdleDelay ()
    {

        
        enemy_Anim.SetBool("isIdle", true);

        yield return new WaitForSeconds(1.25f);

        enemy_Anim.SetBool("isIdle", false);

        CurrentState = EnemyState.ENEMY_ATTACK;

    }

}
