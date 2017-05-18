using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Set
{ 

    private GameObject              enemy;
    [SerializeField] private List<GameObject>   enemyList   = new List<GameObject>();
    [NonSerialized] public static Level         level       = null;

    private bool                    canSpawn    = true;
    private float                   levelTimer  = 60;
    public  float                   currentTime = 0;
    private float                   time;
    private static GameObject       levelGO     = null;
    private float                   ranTime     = 0;
    public  List<GameObject>        enemyObj    = new List<GameObject>();
    public  GameObject              cutScene    = null;


    private void Start()
    {
        FillingEnemyList();
    }



    private void Update()
    {
        StartCoroutine(SpawnEnemy());
        LevelProgress();
    }




    public static void LoadLevel()
    {
        if(levelGO)
            Destroy(levelGO);

        levelGO = ResourceManager.Create("Prefab/Level/MasterLevel");
        level   = levelGO.GetComponent<Level>();
    }
    



    IEnumerator SpawnEnemy()
    {
        if (canSpawn == true)
        {
            canSpawn  = false;
            yield return new WaitForSeconds(ranTime);
            ManageEnemy();
            GameObject newEnemy = Instantiate(enemy, GettingEnemySpawnLocation().position, Quaternion.identity);
            enemyObj.Add(newEnemy);
            canSpawn = true;
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




    private void LevelProgress()
    {
        if (currentTime < levelTimer)
        {
            currentTime += Time.deltaTime;
            time = currentTime / levelTimer;
            float displayTime = time * 100;
            displayTime = Mathf.RoundToInt(displayTime);
            Game.Inst.hud.progressText.text = displayTime.ToString() + "%";
            Game.Inst.hud.progressBar.fillAmount = time;
        }
        else if (currentTime >= levelTimer) //and  player health is greater than 0
        {
            Game.Inst.dataManager.levelUnlocked++;
            cutScene    = ResourceManager.Create("Prefab/Misc/WinCutScene");
            Renderer r  = cutScene.GetComponent<Renderer>();
            MovieTexture movie = (MovieTexture)r.material.mainTexture;
            if (cutScene && r)
            {
                movie.Play();
                movie.loop = true;
                Game.Inst.hud.GameCondition("Win");
            }
            DataUtility.SaveData();
        }
    }


    private void ManageEnemy()
    {
        int levelIndex = Game.Inst.dataManager.levelUnlocked;
        switch (levelIndex)
        {
            case 0:
                enemy = enemyList[2];
                ranTime = UnityEngine.Random.Range(3.0f, 5.0f);
                break;
            case 1:
                enemy = enemyList[1];
                break;
            case 2:
                enemy = enemyList[2];
                break;
            case 3:
                enemy = enemyList[UnityEngine.Random.Range(0, enemyList.Count)];
                break;
        }
    }
}
