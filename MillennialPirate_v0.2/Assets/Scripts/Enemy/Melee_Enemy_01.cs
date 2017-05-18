using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Melee_Enemy_01 : EnemyClass
{
  
    protected bool inRange = false;
    protected float yOffset = -1.9f;
    public EnemyHealthBar enemyHealth = null;

    void Start ()
    {
        Init();
        enemyHealth.enemyMaxHealth = 3;
    }



	public override void Update ()
    {
        Vector2 hpPos = Game.Inst.UICamera.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 3.5f));
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
                enemy_Anim.SetBool("hasBeenKilled", true);
                StopAllCoroutines();
                break;
        }



        if (enemyHealth.enemyCurrentHealth <= 0)
        {
            ChangeState(EnemyState.ENEMY_DEATH);
            Destroy(enemyHealthBar);
        }
        else
            enemyHealthBar.transform.position = hpPos;

            
    }



    protected void ChangeState(EnemyState newState)
    {
        enemy_Anim.SetBool("attackTrigger", false);
        enemy_Anim.SetBool("isHit", false);
        enemy_Anim.SetBool("isIdle", false);
        CurrentState = newState;
    }



    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }




    public void CheckState(float distance)
    {
        if (DistanceToTarget(player.position) >= distance)
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




    protected void Init()
    {
        ChangingDirection();
        StartCoroutine(MoveTowardTarget(player.position));
        CurrentState = EnemyState.ENEMY_WALKING;
        EnemyHealthBar();
        enemyHealth = enemyHealthBar.GetComponent<EnemyHealthBar>();
    }
}
