using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    //chached references
    private SceneManagerController sceneManagerController;
    [SerializeField] private GameObject successText;
    private GameSession gameSession;
    private bool hasFinishedLevel = false;

    //configuration parameters
    [Range(0f, 1f)] [SerializeField] private float frozenTime=0.2f;    

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFinishedLevel)
        {
            hasFinishedLevel = true;
            StartCoroutine(ProcessNextSceneTranslation());
        }        
    }

    IEnumerator ProcessNextSceneTranslation()
    {        
        GameObject newSuccessCanvas = Instantiate(successText)as GameObject;
        Time.timeScale = frozenTime;
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1;
        Destroy(newSuccessCanvas);
        gameSession.IncrementTheLevelNumber();
        sceneManagerController.LoadNextScene();
    }
}
