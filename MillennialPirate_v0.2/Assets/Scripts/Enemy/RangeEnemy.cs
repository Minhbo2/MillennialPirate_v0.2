using System.Collections;
using UnityEngine;

public class RangeEnemy : MonoBehaviour {
    
    private enum RangeEnemyState { spawn, walk, attack, idle, die, getHit }
    private RangeEnemyState r_Enemy     = RangeEnemyState.spawn;

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

    [SerializeField] private float startDelayTime;
    private float startDelayTimer = 1.5f;

    [SerializeField] private float attackDelayTime;
    private float attackDelayTimer = 2f;

    //public  float           healthbarYOffset = 5.0f;
    //private GameObject      healthBarGO;


    private void Awake()
    {
        player  = GameObject.Find("Player").GetComponent<Transform>();
    }



    private void Start()
    {
        Flip();
    }





    private void Update()
    {
        switch (r_Enemy)
        {
            case RangeEnemyState.spawn:
                startDelayTimer += Time.deltaTime;

                if (startDelayTimer > startDelayTime)
                    ChangeCurrentState(RangeEnemyState.walk);

                break;
            case RangeEnemyState.idle:

                break;
            case RangeEnemyState.walk:
                attackDelayTimer += Time.deltaTime;

                if (attackDelayTimer > attackDelayTime)
                {
                    ChangeCurrentState(RangeEnemyState.attack);
                    attackDelayTimer = 0;
                }
                break;
            case RangeEnemyState.attack:
                // Wait for attack animation to be complete
                break;
            case RangeEnemyState.getHit:

                break;
            case RangeEnemyState.die:

                break;
        }
    }




    public void OnAttackCompleteAnimEvent()
    {
        ChangeCurrentState(RangeEnemyState.walk);
    }




    private void ChangeCurrentState(RangeEnemyState newState)
    {
        r_Enemy = newState;

        enemyAnim.SetBool("Idle", false);
        enemyAnim.SetBool("Attack", false);
        enemyAnim.SetBool("Walk", false);
        enemyAnim.SetBool("Die", false);
        enemyAnim.SetBool("GetHit", false);

        StopAllCoroutines();

        if (newState == RangeEnemyState.idle)
            enemyAnim.SetBool("Idle", true);
        else if (newState == RangeEnemyState.walk)
        {
            enemyAnim.SetBool("Walk", true);
            StartCoroutine("MoveTowardTarget", SetTarget());
        }
        else if (newState == RangeEnemyState.attack)
            enemyAnim.SetBool("Attack", true);
        else if(newState == RangeEnemyState.die)
            enemyAnim.SetBool("Die", true);
        else if (newState == RangeEnemyState.getHit)
            enemyAnim.SetBool("GetHit", true);
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
    }




    public void SpawnArrow()
    {
        arrowPrefab = ResourceManager.Create("Prefab/Misc/Arrow");
        arrowPrefab.transform.position = arrowAnchor.position;
    } 
}
