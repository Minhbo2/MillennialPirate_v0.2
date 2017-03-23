using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level01Set : Set
{

    private GameObject enemy;

    private Transform eSpawn1;
    private Transform eSpawn2;

    private bool canSpawn = true;

    public int index = 0;

	// Use this for initialization
	void Start ()
    {
        eSpawn1 = GameObject.Find("EnemySpawn1").transform;
        eSpawn2 = GameObject.Find("EnemySpawn2").transform;

        enemy = Resources.Load("Prefab/Enemy/Melee_Enemy") as GameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(SpawnEnemy());
	}

    IEnumerator SpawnEnemy()
    {
        if (canSpawn == true)
        {
            canSpawn = false;

            index = Random.Range(1, 3);

            float ranTime = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(ranTime);

            if(index == 1)
            {
                GameObject temp = Instantiate(enemy, eSpawn1.position, Quaternion.identity);
                temp.transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            else if(index == 2)
            {
                GameObject temp = Instantiate(enemy, eSpawn2.position, Quaternion.identity);
                temp.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            canSpawn = true;
        }


    }
}
