using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Set {

    public static int levelComplete;


    private void Start()
    {
        LoadLevel(levelComplete);
    }

    private void LoadLevel(int level)
    {
        ResourceManager.Create("Prefab/Level/Level" + level);
    }
}
