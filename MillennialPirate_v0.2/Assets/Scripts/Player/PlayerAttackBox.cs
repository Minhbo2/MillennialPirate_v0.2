﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour {

    GameObject gc;

	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (gc.GetComponent<Player>().heavyAttacking == false || gc.GetComponent<Player>().knockBacking == false)
        {
            if (other.gameObject.tag == "MeleeEnemy")
            {
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "RangeEnemy")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
