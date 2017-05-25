using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Transform   player  = null;
    private Vector2     direction;
    private float speed         = 3f;


    private void Awake()
    {
        player      = GameObject.Find("Player").GetComponent<Transform>();
        direction   = player.position;
    }


    void Start()
    {
        Flip();
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
    }
    



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            var hitVFX = ResourceManager.Create("Prefab/Misc/HitVFX");
            hitVFX.transform.position = transform.position;

            if (!Player.isDodging && !Player.isInvul)
            {
                Player playerScript = GameObject.Find("GameController").GetComponent<Player>();
                playerScript.SwitchPlayerState(PlayerState.PLAYER_HIT);
                PlayerHealth.currentHealth--;

            }

            Destroy(this.gameObject);
        }
    }


    private void Flip()
    {
        float playerXPos = player.transform.position.x;

        if (playerXPos > transform.position.x)
            transform.GetComponent<SpriteRenderer>().flipX = true;
    }
}
