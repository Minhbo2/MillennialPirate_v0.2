
using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField]
    private         GameObject[]    spawnPos;
    public static   GameObject      hudSet;
    public Player CurrentPlayer;


    private void Start()
    {
        if (!hudSet)
            hudSet = transform.FindChild("HUDSet").gameObject;
    }
    /// <summary>
    /// getting a random location from the array
    /// remember to assign locations for every level prefab
    /// </summary>
    public GameObject RandonSpawnPosition()
    {
        GameObject randomPos = null;

        if (spawnPos.Length > 0)
            randomPos = spawnPos[Random.Range(0, spawnPos.Length)];

        return randomPos;
    }
}
