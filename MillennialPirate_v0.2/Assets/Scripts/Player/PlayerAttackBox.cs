using System.Collections;
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
                other.gameObject.GetComponent<HeavyEnemy>().health--;
            }
        }
    }
}
