using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class HighscoreData
{
    public float[] _highscores = {};
}

public static class SaveLoadSystem
{
    public static void SaveHighscore(HighscoreData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/highscore.blnc";
        FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate);

        formatter.Serialize(fstream, data);
        fstream.Close();
    }

    public static HighscoreData LoadHighscore()
    {
        string filePath = Application.persistentDataPath + "/highscore.blnc";
        if (!File.Exists(filePath)) return null;


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fstream = new FileStream(filePath, FileMode.Open);

        HighscoreData highscore = formatter.Deserialize(fstream) as HighscoreData;
        fstream.Close();

        return highscore;
    }
}
