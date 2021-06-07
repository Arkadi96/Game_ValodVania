using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private int lifeCount = 3;
    private int currentLevelNumber=1;
    private bool isAlive = true;

    //cached references    
    private SceneManagerController sceneManager;
    [SerializeField] private GameObject wastedText;    

    private void Awake()
    {
        int gameSessionsCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionsCount>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCount>=1)
        {
            return;
        }
        else if(isAlive)
        {
            isAlive = false;
            ProcessPlayerDeath();
        }
        else
        {
            Debug.Log("nor has life nor is alive");
            return;
        }
    }

    private void ProcessPlayerDeath()
    {        
        StartCoroutine(ProcessDeathSceneTranslation());        
    }

    IEnumerator ProcessDeathSceneTranslation()
    {
        GameObject newWastedTextCanvas = Instantiate(wastedText) as GameObject;
        yield return new WaitForSeconds(2.0f);
        Destroy(newWastedTextCanvas);
        sceneManager.LoadGameOver();
        ResetGameSession();
    }

    private void ResetGameSession()
    {
        Destroy(gameObject);
    }

    public void DecrementTheLife()
    {
        lifeCount--;
        StartCoroutine(ResetGameScene());
    }

    IEnumerator ResetGameScene()
    {
        GameObject newWastedTextCanvas = Instantiate(wastedText) as GameObject;        
        yield return new WaitForSeconds(2.0f);
        Destroy(newWastedTextCanvas);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void IncrementTheLife()
    {
        lifeCount++;
    }

    public void IncrementTheLevelNumber()
    {
        currentLevelNumber++;
    }

    public int GetPlayerLivesCount()
    {
        return lifeCount;
    }

    public int GetLevelNumber()
    {
        return currentLevelNumber;
    }
}
