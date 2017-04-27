using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Enemy_Attack : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            if (!Player.isDodging)
                PlayerHealth.currentHealth--;
        }
    }
 
}
