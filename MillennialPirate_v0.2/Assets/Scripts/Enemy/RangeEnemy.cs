using System.Collections;
using UnityEngine;

public class RangeEnemy : MonoBehaviour {

    private enum RangeEnemyState { walk, attack, idle, die, getHit }
    [SerializeField]
    private RangeEnemyState r_Enemy     = RangeEnemyState.idle;

    private int             health      = 10;
    private float           speed       = 1.5f;
    [SerializeField]
    private Transform       arrowAnchor;
    private Transform       player      = null;
    [SerializeField]
    private Animator        enemyAnim;
    [SerializeField]
    private GameObject      arrowPrefab;
    [SerializeField]
    private SpriteRenderer  enemySpriteRenderer;
    public  bool            getHit      = false;
    public  bool            isIdle      = false;
    public  bool            isAttacking = false;
    public  bool            isWalking   = false;



    private void Awake()
    {
        player  = GameObject.Find("Player").GetComponent<Transform>();
    }



    private void Start()
    {
        Flip();
        ChangeCurrentState(RangeEnemyState.idle);
    }





    private void Update()
    {
        switch (r_Enemy)
        {
            case RangeEnemyState.idle:
                if (getHit)
                    ChangeCurrentState(RangeEnemyState.getHit);
                else if (health <= 0)
                    ChangeCurrentState(RangeEnemyState.die);
                else if (isWalking)
                    ChangeCurrentState(RangeEnemyState.walk);
                else if (isAttacking)
                    ChangeCurrentState(RangeEnemyState.attack);
                break;
            case RangeEnemyState.walk:
                if (getHit)
                    ChangeCurrentState(RangeEnemyState.getHit);
                else if (isIdle)
                    ChangeCurrentState(RangeEnemyState.idle);
                else if (health <= 0)
                    ChangeCurrentState(RangeEnemyState.die);
                else if (isAttacking)
                    ChangeCurrentState(RangeEnemyState.attack);
                break;
            case RangeEnemyState.attack:
                if (getHit)
                    ChangeCurrentState(RangeEnemyState.getHit);
                else if (isIdle)
                    ChangeCurrentState(RangeEnemyState.idle);
                else if (health <= 0)
                    ChangeCurrentState(RangeEnemyState.die);
                else if (isWalking)
                    ChangeCurrentState(RangeEnemyState.walk);
                break;
            case RangeEnemyState.getHit:
                if (isIdle)
                    ChangeCurrentState(RangeEnemyState.idle);
                else if (health <= 0)
                    ChangeCurrentState(RangeEnemyState.die);
                else if (isWalking)
                    ChangeCurrentState(RangeEnemyState.walk);
                else if (isAttacking)
                    ChangeCurrentState(RangeEnemyState.attack);
                break;
        }
    }






    private void ChangeCurrentState(RangeEnemyState newState)
    {
        r_Enemy = newState;

        enemyAnim.SetBool("Idle", false);
        enemyAnim.SetBool("Attack", false);
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Die", false);

        getHit      = false;
        isIdle      = false;
        isAttacking = false;
        isWalking   = false;

        StopAllCoroutines();

        if (newState == RangeEnemyState.idle)
        {
            enemyAnim.SetBool("Idle", true);
            isWalking = true;
        }
        else if (newState == RangeEnemyState.walk)
        {
            enemyAnim.SetBool("Walk", true);
            StartCoroutine("MoveTowardTarget", SetTarget());
        }
        else if (newState == RangeEnemyState.attack)
        {
            enemyAnim.SetBool("Attack", true);
        }
        else if(newState == RangeEnemyState.die)
        {
            enemyAnim.SetBool("Die", true);
        }
        else if (newState == RangeEnemyState.getHit)
        {
            enemyAnim.SetBool("Get Hit", true);
            isIdle = true;
        }
    }




    private void Flip()
    {
        if (player)
        {
            float playerXPos        = player.position.x;
            float transformXPos     = transform.position.x;
            float yRot              = transform.eulerAngles.y;

            if (transformXPos < playerXPos)
            {
                if (yRot == 0)
                {
                    yRot = 180;
                    transform.eulerAngles = new Vector2(0, yRot);
                }
            }
        }
    }





    private Vector2 SetTarget()
    {
        int moveDistance;
        float xPos = player.position.x;

        if (xPos > transform.position.x)
            moveDistance = 2;
        else
            moveDistance = -2;

        Vector2 target = new Vector2(transform.position.x + moveDistance, transform.position.y);
        return target;
    }




    private float DistanceToTarget(Vector2 target)
    {
        float distanceToTarget = Vector2.Distance(target, transform.position);
        return distanceToTarget;
    }




    private IEnumerator MoveTowardTarget(Vector2 target)
    {
        while (DistanceToTarget(target) >= 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return DistanceToTarget(target);
        }
        isWalking   = false;
        isAttacking = true;
    }





    public void SpawnArrow()
    {
        arrowPrefab = ResourceManager.Create("Prefab/Enemy/Arrow");
        arrowPrefab.transform.position = arrowAnchor.position;
        isWalking   = true;
        isAttacking = false;
    }
}
