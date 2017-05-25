using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Set
{ 

    private GameObject              enemy;
    [SerializeField] private List<GameObject>   enemyList   = new List<GameObject>();
    [NonSerialized] public static Level         level       = null;

    [SerializeField]
    private bool                    canSpawn    = true;
    private float                   levelTimer  = 60;
    public  float                   currentTime = 0;
    private float                   time;
    private static GameObject       levelGO     = null;
    private float                   ranTime     = 0;
    public  List<GameObject>        enemyObj    = new List<GameObject>();
    public  GameObject              cutScene    = null;

    public  bool                    isLevelDone = false;

    public AudioClip                loseSFX;
    public AudioSource              audioSource;


    public AudioClip                gameLevelClip;

    private void Start()
    {
        FillingEnemyList();
        ManageEnemy();
        audioSource.clip = gameLevelClip;
        audioSource.Play();
    }



    private void Update()
    {
        StartCoroutine(SpawnEnemy());
        LevelProgress();
    }




    public void LoadLevel()
    {
        isLevelDone = false;

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
            ManageEnemy();
            yield return new WaitForSeconds(ranTime);
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
        if (isLevelDone == false)
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
            else
            {
                int levelUnlock = Game.Inst.dataManager.levelUnlocked;
                if (levelUnlock <= Game.Inst.dataManager.levelSelected)
                    Game.Inst.dataManager.levelUnlocked++;

                DataUtility.SaveData();
                cutScene = ResourceManager.Create("Prefab/Misc/WinCutScene");
                Renderer r = cutScene.GetComponent<Renderer>();
                MovieTexture movie = (MovieTexture)r.material.mainTexture;
                if (cutScene && r)
                {
                    movie.Play();
                    movie.loop = true;
                    Game.Inst.hud.GameCondition("Win");
                }
                audioSource.PlayOneShot(loseSFX);
                isLevelDone = true;
            }
        }
    }


    private void ManageEnemy()
    {
        int levelIndex = Game.Inst.dataManager.levelSelected;
        switch (levelIndex)
        {
            case 0:
                enemy = enemyList[0];
                ranTime = UnityEngine.Random.Range(3.0f, 5.0f);
                gameLevelClip = Resources.Load("Sounds/BGM/Level01_BGM") as AudioClip;
                break;
            case 1:
                enemy = enemyList[1];
                ranTime = UnityEngine.Random.Range(3.0f, 5.0f);
                gameLevelClip = Resources.Load("Sounds/BGM/Level02_BGM") as AudioClip;
                break;
            case 2:
                enemy = enemyList[2];
                ranTime = UnityEngine.Random.Range(3.0f, 5.0f);
                gameLevelClip = Resources.Load("Sounds/BGM/Level03_BGM") as AudioClip;
                break;
            case 3:
                enemy = enemyList[UnityEngine.Random.Range(0, enemyList.Count)];
                ranTime = UnityEngine.Random.Range(3.0f, 5.0f);
                gameLevelClip = Resources.Load("Sounds/BGM/Level04_BGM") as AudioClip;
                break;
            case 4:
                enemy = enemyList[UnityEngine.Random.Range(0, enemyList.Count)];
                ranTime = UnityEngine.Random.Range(2.5f, 4.5f);
                gameLevelClip = Resources.Load("Sounds/BGM/Level05_BGM") as AudioClip;
                break;
        }
    }
}
