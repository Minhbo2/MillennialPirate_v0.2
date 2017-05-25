using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Range_Weapon : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            if (!Player.isDodging && !Player.isInvul)
            {
                Player playerScript = GameObject.Find("GameController").GetComponent<Player>();
                playerScript.SwitchPlayerState(PlayerState.PLAYER_HIT);
                PlayerHealth.currentHealth--;
            }
        }
    }

}
