using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerState
{
    PLAYER_IDLE,
    PLAYER_ATTACK_LIGHT,
    PLAYER_ATTACK_CHAIN_01,
    PLAYER_ATTACK_CHAIN_02,
    PLAYER_ATTACK_CHAIN_03,
    PLAYER_ATTACK_HEAVY,
    PLAYER_DODGE,
    PLAYER_HIT,
    PLAYER_DEAD,
    PLAYER_KNOCKBACK
}

public class Player : MonoBehaviour
{

    private GameObject player;

    public Animator playerAnim;

    private Animation playerIdle;
    private Animation playerLightAttack;
    private Animation playerHeavyAttack;
    private Animation playerDodge;

    public bool heavyAttacking = false;
    public bool knockBacking = false;

    private AudioSource playerAudioSource;

    private AudioClip lightAttackSound;
    private AudioClip heavyAttackSound;

    private bool isSoundPlaying = false;

    public static bool isDodging = false;

    private bool isHoldingLeft = false;
    private bool isHoldingRight = false;

    public int attackIndex = 1;

    private bool knockBackReady = false;

    private float knockBackTimer = 5.0f;
    private float knockBackTemp = 0.0f;

    private void Start()
    {
        player = GameObject.Find("Player");

        playerAudioSource = GameObject.Find("Player").GetComponent<AudioSource>();

        playerAnim = GameObject.Find("Player").GetComponent<Animator>();

        playerLightAttack = Resources.Load("Animations/Player/Player_Attack_Light") as Animation;
        playerHeavyAttack = Resources.Load("Animations/Player/Player_Attack_Heavy") as Animation;

        //player.GetComponent<SpriteRenderer>().flipX = true;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);

        lightAttackSound = Resources.Load("Sounds/Player_Light_Attack") as AudioClip;
        heavyAttackSound = Resources.Load("Sounds/Player_Heavy_Attack") as AudioClip;

        
    } 

    // The player's current state
    PlayerState CurrentState = PlayerState.PLAYER_IDLE;

    // Update is called once per frame
    void Update()
    {

        // Player STATE MACHINE
        switch (CurrentState)
        {
            case PlayerState.PLAYER_IDLE:
                playerAnim.SetBool("LightAttack", false);
                playerAnim.SetBool("Attack", false);
                playerAnim.SetBool("Attack_02", false);
                playerAnim.SetBool("Attack_03", false);
                playerAnim.SetBool("HeavyAttack", false);
                playerAnim.SetBool("Dodge", false);
                isSoundPlaying = false;
                StopAllCoroutines();

                break;

            case PlayerState.PLAYER_DODGE:

                StartCoroutine(DodgeDuration());

                break;

            case PlayerState.PLAYER_ATTACK_LIGHT:

                //StartCoroutine(LightAttackDuration());
                StartCoroutine(AttackChain());

                break;

            case PlayerState.PLAYER_ATTACK_CHAIN_01:


                break;

            case PlayerState.PLAYER_ATTACK_CHAIN_02:


                break;

            case PlayerState.PLAYER_ATTACK_CHAIN_03:

                break;

            case PlayerState.PLAYER_ATTACK_HEAVY:

                StartCoroutine(HeavyAttackDuration());

                break;

            case PlayerState.PLAYER_HIT:
                break;

            case PlayerState.PLAYER_DEAD:
                break;

            case PlayerState.PLAYER_KNOCKBACK:

                StartCoroutine(KnockBackDuration());
                knockBackReady = false;

                break;

        }

        CheckState();

        KeyControls();

        KnockBackCooldown();

    }

    void SwitchPlayerState (PlayerState newState)
    {
        CurrentState = newState;
    }

    public void _LookRight()
    {
        if(heavyAttacking == false || knockBacking == false)
        {
            SwitchPlayerState(PlayerState.PLAYER_IDLE);
            //player.GetComponent<SpriteRenderer>().flipX = true;
        }


    }

    public void _LookLeft()
    {
        if (heavyAttacking == false || knockBacking == false)
        {
            SwitchPlayerState(PlayerState.PLAYER_IDLE);
            //player.GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    public void _Dodge ()
    {
        //Player dodges
        SwitchPlayerState(PlayerState.PLAYER_DODGE);
    }

    public void _LightAttack ()
    {
        //Player light attack
        if (heavyAttacking == false || knockBacking == false)
        {
            if (isHoldingLeft == true || isHoldingRight == true)
            {
                if (knockBackReady == true)
                {
                    //knockback
                    Debug.Log("knockbacks");
                    SwitchPlayerState(PlayerState.PLAYER_KNOCKBACK);
                }
                else if (knockBackReady == false)
                {
                    SwitchPlayerState(PlayerState.PLAYER_ATTACK_LIGHT);
                    playerAudioSource.clip = lightAttackSound;
                }
            }

            else if (isHoldingLeft == false || isHoldingRight == false)
            {

                SwitchPlayerState(PlayerState.PLAYER_ATTACK_LIGHT);
                playerAudioSource.clip = lightAttackSound;
            }
        }

    }



    public void _HeavyAttack ()
    {
        //Player heavy attack
        SwitchPlayerState(PlayerState.PLAYER_ATTACK_HEAVY);
        playerAudioSource.clip = heavyAttackSound;

    }


    public void _LeftButtonDown ()
    {
        isHoldingLeft = true;
        Debug.Log("holding L");
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void _RightButtonDown ()
    {
        isHoldingRight = true;
        Debug.Log("holding R");
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void _LeftButtonUp ()
    {
        isHoldingLeft = false;
    }

    public void _RightButtonUp()
    {
        isHoldingRight = false;
    }

    void KnockBackCooldown ()
    {

        if(knockBackReady == false)
        {

            knockBackTemp += Time.deltaTime;

            if(knockBackTemp >= knockBackTimer)
            {
                knockBackReady = true;
                knockBackTemp = 0;
            }
        }
    }


    IEnumerator LightAttackDuration ()
    {
        if(isSoundPlaying == false)
        {
            if (isDodging == false)
            {
                isSoundPlaying = true;
                playerAudioSource.PlayOneShot(lightAttackSound);
            }
        }

        playerAnim.SetBool("LightAttack", true);

        yield return new WaitForSeconds(0.5f);

        playerAnim.SetBool("LightAttack", false);

        SwitchPlayerState(PlayerState.PLAYER_IDLE);

        isSoundPlaying = false;

    }

    IEnumerator AttackChain ()
    {
        if (heavyAttacking == false)
        {
            if (attackIndex == 1)
            {
                playerAnim.SetBool("Attack", true);

                yield return new WaitForSeconds(0.30f);

                SwitchPlayerState(PlayerState.PLAYER_IDLE);

                playerAnim.SetBool("Attack", false);

                attackIndex++;

            }
            else if (attackIndex == 2)
            {
                playerAnim.SetBool("Attack_02", true);

                yield return new WaitForSeconds(0.30f);

                SwitchPlayerState(PlayerState.PLAYER_IDLE);

                playerAnim.SetBool("Attack_02", false);

                attackIndex++;

            }
            else if (attackIndex == 3)
            {
                playerAnim.SetBool("Attack_03", true);

                yield return new WaitForSeconds(0.30f);

                SwitchPlayerState(PlayerState.PLAYER_IDLE);

                playerAnim.SetBool("Attack_03", false);

                attackIndex = 1;

            }
        }

    }

    IEnumerator HeavyAttackDuration ()
    {
        if (isSoundPlaying == false)
        {
            if (isDodging == false)
            {
                isSoundPlaying = true;
                playerAudioSource.PlayOneShot(heavyAttackSound);
            }
        }

        playerAnim.SetBool("HeavyAttack", true);
        heavyAttacking = true;

        yield return new WaitForSeconds(0.95f);

        playerAnim.SetBool("HeavyAttack", false);
        heavyAttacking = false;

        SwitchPlayerState(PlayerState.PLAYER_IDLE);

        isSoundPlaying = false;
    }

    IEnumerator DodgeDuration()
    {
        isDodging = true;

        playerAnim.SetBool("Dodge", true);

        yield return new WaitForSeconds(0.44f);

        isDodging = false;

        SwitchPlayerState(PlayerState.PLAYER_IDLE);
    }

    IEnumerator KnockBackDuration()
    {
        playerAnim.SetBool("KnockBack", true);
        knockBacking = true;

        yield return new WaitForSeconds(0.38f);

        playerAnim.SetBool("KnockBack", false);
        knockBacking = false;

        SwitchPlayerState(PlayerState.PLAYER_IDLE);
    }

    


    private void KeyControls ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (heavyAttacking == false || knockBacking == false)
            {
                SwitchPlayerState(PlayerState.PLAYER_IDLE);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                //player.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (heavyAttacking == false || knockBacking == false)
            {
                SwitchPlayerState(PlayerState.PLAYER_IDLE);
                //player.GetComponent<SpriteRenderer>().flipX = true;
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchPlayerState(PlayerState.PLAYER_DODGE);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (heavyAttacking == false || knockBacking == false)
            {
                if (isHoldingLeft == true || isHoldingRight == true)
                {
                    if (knockBackReady == true)
                    {
                        //knockback
                        Debug.Log("knockbacks");
                        SwitchPlayerState(PlayerState.PLAYER_KNOCKBACK);
                    }
                    else if (knockBackReady == false)
                    {
                        SwitchPlayerState(PlayerState.PLAYER_ATTACK_LIGHT);
                        playerAudioSource.clip = lightAttackSound;
                    }
                }

                else if (isHoldingLeft == false || isHoldingRight == false)
                {

                    SwitchPlayerState(PlayerState.PLAYER_ATTACK_LIGHT);
                    playerAudioSource.clip = lightAttackSound;
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            SwitchPlayerState(PlayerState.PLAYER_ATTACK_HEAVY);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            SwitchPlayerState(PlayerState.PLAYER_DEAD);
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            SwitchPlayerState(PlayerState.PLAYER_HIT);
        }
            
    }

    private void CheckState ()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log(CurrentState);
        }
    }
}
