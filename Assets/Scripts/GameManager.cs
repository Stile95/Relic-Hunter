using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Lives = 3;
    public int Coins = 0;

    public LivesGridController LivesGridController;
    public Text CoinsText;

    public int CoinsForNewLife;

    private void Awake()
    {
        Instance = this;

        Lives = SaveLoadManager.LoadValue(SaveKey.Lives);
        LivesGridController.RefreshLifeImages(Lives);

        Lives = SaveLoadManager.LoadValue(SaveKey.Coins);
        CoinsText.text = Coins.ToString();
    }
    public void UpdateCoins(int value)
    {
        Coins += value;
        CoinsText.text = Coins.ToString();

        if(Coins >= CoinsForNewLife)
        {
            Coins = 0;
            UpdateLives(1);
        }

        SaveLoadManager.SaveValue(SaveKey.Coins, Coins);
    }
    public void UpdateLives(int value)
    {
        Lives += value;
        LivesGridController.RefreshLifeImages(Lives);

        
        SaveLoadManager.SaveValue(SaveKey.Lives, Lives);
    }


}






    
