using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataUtility : MonoBehaviour {

    // Use this for initialization
    public static void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/game.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open((Application.persistentDataPath + "/score.dat"), FileMode.Open);
            Data loadedData = (Data)bf.Deserialize(file);
            file.Close();

            LevelSelectionSet.levelCompleted = loadedData.levelCompleted;
            LevelManager.levelComplete = loadedData.levelCompleted;
        }
    }

    public static void SaveData()
    {
        Data newData = new Data();
        newData.levelCompleted = LevelSelectionSet.levelCompleted;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/score.dat");
        bf.Serialize(file, newData);
        file.Close();
    }

}
