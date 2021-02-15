using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum SaveKey
{

    Lives,
    Coins

}
public static class SaveLoadManager 
{

    public static void SaveValue(SaveKey saveKey, int value)
    {
        PlayerPrefs.SetInt(saveKey.ToString(), value);

    }

    public static int LoadValue(SaveKey saveKey)
    {
        int loadedValue = PlayerPrefs.GetInt(saveKey.ToString(), 0);

        return loadedValue;
    }

    public static void DeleteAllSavedKey()
    {
        PlayerPrefs.DeleteKey(SaveKey.Lives.ToString());
        PlayerPrefs.DeleteKey(SaveKey.Coins.ToString());


    }
}
