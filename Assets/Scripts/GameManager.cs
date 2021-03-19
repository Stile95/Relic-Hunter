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
    public int MaximumLives = 3;

    public LivesGridController LivesGridController;
    public Text CoinsText;

    public Canvas DeathScreen;

    public int CoinsForNewLife;

    private void Awake()
    {
        Instance = this;
        DeathScreen.enabled = false;

        //Lives = SaveLoadManager.LoadValue(SaveKey.Lives);
        LivesGridController.RefreshLifeImages(Lives);

        //Coins = SaveLoadManager.LoadValue(SaveKey.Coins);
        CoinsText.text = Coins.ToString();
    }
    public void UpdateCoins(int value)
    {
        Coins++;

        if(Coins >= CoinsForNewLife && Lives < MaximumLives)
        {
            Coins = 0;
            UpdateLives(1);
        }

        CoinsText.text = Coins.ToString();
        SaveLoadManager.SaveValue(SaveKey.Coins, Coins);
    }

    public void UpdateLives(int value)
    {
        Lives += value;
        LivesGridController.RefreshLifeImages(Lives);

        SaveLoadManager.SaveValue(SaveKey.Lives, Lives);
    }


}






    
