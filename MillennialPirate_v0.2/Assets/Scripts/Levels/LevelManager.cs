using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Set
{ 

    private GameObject              enemy;
    [SerializeField]
    private List<GameObject>        enemyList   = new List<GameObject>();
    private static GameObject       level       = null;
    private bool                    canSpawn    = true;
    public  static int              levelIndex  = 0;
    private float                   levelTimer  = 60;
    private float                   currentTime = 0;
    private float                   time;


    private void Start()
    {
        FillingEnemyList();
    }



    private void Update()
    {
        StartCoroutine(SpawnEnemy());
        LevelTime();
    }




    public static void LoadLevel(int index)
    {
        level   = ResourceManager.Create("Prefab/Level/Level" + index);
    }




    IEnumerator SpawnEnemy()
    {
        if (canSpawn == true)
        {
            canSpawn        = false;
            float ranTime   = Random.Range(3.0f, 5.0f);
            yield return new WaitForSeconds(ranTime);

            enemy           = enemyList[Random.Range(0, enemyList.Count)];
            Instantiate(enemy, GettingEnemySpawnLocation().position, Quaternion.identity);
            canSpawn        = true;
        }
    }




    /// <summary>
    /// accessing the level script attaching to the leve prefab
    /// grabbing a function return a random spawn location
    /// </summary>
    private Transform GettingEnemySpawnLocation()    {
        Transform enemySpawnLocation = null;

        if (level)
            enemySpawnLocation = level.GetComponent<Level>().RandonSpawnPosition().transform;

        return enemySpawnLocation;   
    }



    /// <summary>
    /// go to the resource folder and fill the list with enemy prefab
    /// </summary>
    private void FillingEnemyList()
    {
        bool isDone = false;
        GameObject enemyObj = null;
        int counter = 0;

        while (!isDone)
        {
            enemyObj = Resources.Load("Prefab/Enemy/Enemy" + counter) as GameObject;

            if (!enemyObj)
                isDone = true;
            else
                enemyList.Add(enemyObj);

            counter++;
        }
    }




    private void LevelTime()
    {
        if (currentTime < levelTimer)
        {
            currentTime += Time.deltaTime;
            time = currentTime / levelTimer;
            HUDSet.Inst.progressText.text = currentTime.ToString() + "/60";
            HUDSet.Inst.progressBar.fillAmount = time;
        }
        else if (currentTime >= levelTimer) //and  player health is greater than 0
        {
            if (!HUDSet.Inst.winScreen.activeInHierarchy)
            {
                //levelIndex++;
                HUDSet.Inst.GameCondition("Win");
            }
        }
    }
}
