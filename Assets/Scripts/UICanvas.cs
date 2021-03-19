using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour
{
   
    public void PlayAgain()
    {
        SaveLoadManager.DeleteAllSavedKey();
        SceneManager.LoadScene(1);

    }

    public void MainMenu()
    {

        SceneManager.LoadScene(0);

    }


    public void Quit()
    {

        Application.Quit();
    }
}
