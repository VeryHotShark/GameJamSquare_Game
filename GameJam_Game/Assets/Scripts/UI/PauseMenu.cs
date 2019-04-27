using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject Canvas;
     void Update()
    {
       
 if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Closing();
            else
                Opening();

        }
    }
    void Closing()
    {
        Time.timeScale = 1;
        paused = false;
        Canvas.active = false;
    }
    void Opening()
    {
        print("yihiuh");
        paused = true;
        Canvas.active = true;
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
