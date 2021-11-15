using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menu : MonoBehaviour
{
    public AudioSource menus ;

    public void Start()
    {
        if (PlayerMovement.mute == 0)
        {
            menus.Play();
        }
        if (PlayerMovement.mute == 1)
        {
            menus.Pause();
            print("in pause");
        }
    }
    
    public void start()

    {
        
        menus.Pause();
        SceneManager.LoadScene(sceneName: "Scene1");
       



    }

    public void options()


    {
        if (PlayerMovement.mute == 1)
        {
            menus.Pause();
            print("in pause");
        }

        SceneManager.LoadScene(sceneName: "Options");

       
    }
    public void HowToPlay()

    {
        if (PlayerMovement.mute == 1)
        {
            menus.Pause();
            print("in pause");
        }
        SceneManager.LoadScene(sceneName: "howToPlay");
        
    }
    public void Back()

    {
        if (PlayerMovement.mute == 1)
        {
            menus.Pause();
            print("in pause");
        }
        SceneManager.LoadScene(sceneName: "Scene2");

    }
    public void doExitGame()
    {
        print("exit");
        Application.Quit();
    }

    public void Credits()

    {
        if (PlayerMovement.mute == 1)
        {
            menus.Pause();
            print("in pause");
        }
        SceneManager.LoadScene(sceneName: "Credits");

    }
    public void Mute()

    {
        if (PlayerMovement.mute == 1)
        {
            PlayerMovement.mute = 0;
            menus.Play();
        }
        else
        {
            PlayerMovement.mute = 1;
            menus.Pause();
        }

    }

    
}
