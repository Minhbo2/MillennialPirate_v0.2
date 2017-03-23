using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected float
    m_health = 5,
    m_speed = 5;

    private Vector2 playerTrans;

    private bool inRange = false;


    void Start ()
    {
        playerTrans = GameObject.Find("Player").transform.position;




    }
	

	void Update ()
    {
        if (inRange == false)
        {
            /* if(GameObject.Find("Level01Set").GetComponent<Level01Set>().index == 1)
             {
                 Vector2.MoveTowards(this.transform.position, playerTrans, 50f);
             }
             else if(GameObject.Find("Level01Set").GetComponent<Level01Set>().index == 2)
             {
                 Vector2.MoveTowards(this.transform.position, playerTrans, 50f);
             }*/

           transform.position = Vector2.MoveTowards(this.transform.position, playerTrans, 1.5f * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;
        }

    }

}
