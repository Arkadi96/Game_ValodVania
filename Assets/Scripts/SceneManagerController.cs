using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    //chached string parameters
    private string MAIN_MENU_SCENE = "Main_Menu";
    private string GAME_OVER_SCENE = "Game_Over";

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(GAME_OVER_SCENE);
    }

    public void LoadCertainScene(string CERTAIN_SCENE_NAME)
    {
        bool sceneExists = false;
        int numberOfScenes = SceneManager.sceneCount;

        for (int i =0; i<=numberOfScenes;i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).name.Equals(CERTAIN_SCENE_NAME))
            {
                sceneExists = true;
            }
        }         

        if (sceneExists)
        {
            SceneManager.LoadScene(CERTAIN_SCENE_NAME);
        }
        else
        {
            Debug.LogError("Cannot find given scene "+CERTAIN_SCENE_NAME);
        }
    }
}
