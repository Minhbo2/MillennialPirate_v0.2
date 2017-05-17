using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        EnemyHealthBar();
    }



	public override void Update ()
    {
        Vector2 hpPos = Game.Inst.UICamera.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 3.5f));
        enemyHealthBar.transform.position = hpPos;
        Debug.Log(CurrentState);
        transform.position = new Vector2(transform.position.x, yOffset);

        switch (CurrentState)
        {
            case EnemyState.ENEMY_WALKING:
                
                if (DistanceToTarget(player.position) <= 1.8f)
                {
                    StopAllCoroutines();
                    CurrentState = EnemyState.ENEMY_ATTACK;
                }
                break;

            case EnemyState.ENEMY_IDLE:
                enemy_Anim.SetBool("isIdle", true);
                StartCoroutine(IdleDelay());
                break;

            case EnemyState.ENEMY_ATTACK:
                enemy_Anim.SetBool("attackTrigger", true);
                StartCoroutine(AttackDelay());
                break;

            case EnemyState.ENEMY_HIT:
                StopAllCoroutines();
                enemy_Anim.SetBool("isHit", true);
                
                break;

            case EnemyState.ENEMY_DEATH:
                StopAllCoroutines();
                enemy_Anim.SetBool("hasBeenKilled", true);
                break;              

        }

        if (health <= 0)
        {
            CurrentState = EnemyState.ENEMY_DEATH;
        }
	}



    void ChangeState(EnemyState newState)
    {
        enemy_Anim.SetBool("attackTrigger", false);
        enemy_Anim.SetBool("isHit", false);
        enemy_Anim.SetBool("isIdle", false);
        enemy_Anim.SetBool("hasBeenKilled", false);
        CurrentState = newState;
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
            ChangeState(EnemyState.ENEMY_WALKING);
        }
        else
            ChangeState(EnemyState.ENEMY_ATTACK);
    }




    protected IEnumerator AttackDelay ()
    {
        yield return new WaitForSeconds(1.20f);
        ChangeState(EnemyState.ENEMY_IDLE);
    }

    protected IEnumerator IdleDelay ()
    {
        yield return new WaitForSeconds(1.25f);
        ChangeState(EnemyState.ENEMY_ATTACK);
    }

}
