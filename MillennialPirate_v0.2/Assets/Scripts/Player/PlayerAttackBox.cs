using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour {

    GameObject gc;
    GameObject player;

	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameController");
        player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (gc.GetComponent<Player>().knockBacking == false)
        {
            if (other.gameObject.tag == "MeleeEnemy")
            {
                other.gameObject.GetComponent<Melee_Enemy_01>().enemyHealth.enemyCurrentHealth -= 1;
            }

            else if (other.gameObject.tag == "RangeEnemy")
            {
                other.gameObject.GetComponent<RangeEnemy>().health--;
            }

            else if (other.gameObject.tag == "Arrow")
            {
                Destroy(other.gameObject);
            }
            else if(other.gameObject.tag == "HeavyMeleeEnemy")
            {
                other.gameObject.GetComponent<HeavyEnemy>().enemyHealth.enemyCurrentHealth -= 1;
            }
        }
        else if(gc.GetComponent<Player>().knockBacking == true)
        {
            if(other.gameObject.tag == "MeleeEnemy" || other.gameObject.tag == "RangeEnemy" || other.gameObject.tag == "HeavyMeleeEnemy")
            {
                if(gc.GetComponent<Player>().isPlayerFacingRight == false)
                {
                    other.transform.position = new Vector2(other.transform.position.x - 6.0f, other.transform.position.y);
                }
                else if(gc.GetComponent<Player>().isPlayerFacingRight == true)
                {
                    other.transform.position = new Vector2(other.transform.position.x + 6.0f, other.transform.position.y);
                }

            }
        }
    }
}
