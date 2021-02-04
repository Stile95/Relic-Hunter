using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score = 0;
    public Text ScoreText;
    public GameObject GameOverCanvas;




    //uzorak dizajna Singleton
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        GameOverCanvas.gameObject.SetActive(false);
    }

    public void UpdateScore(int value)
    {

        Score += value;

        if (Score < 0)
            Score = 0;
        //ternarni if operator
        //        if(da li je tru ili)  ovo ako je istina : ovo ako je laz;
        // Score = Score < 0 ? 0 : Score;

        ScoreText.text = Score.ToString();


    }

    public void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  
}






    
