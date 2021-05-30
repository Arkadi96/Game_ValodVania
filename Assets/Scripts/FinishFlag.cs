using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    //chached references
    private SceneManagerController sceneManagerController;

    //configuration parameters
    [Range(0f, 1f)] [SerializeField] private float frozenTime=0.2f;

    // Start is called before the first frame update
    void Start()
    {
        sceneManagerController = FindObjectOfType<SceneManagerController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger entered");
        StartCoroutine(LoadNextSceneAnimation());        
    }

    IEnumerator LoadNextSceneAnimation()
    {
        Time.timeScale = frozenTime;
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1;
        sceneManagerController.LoadNextScene();
    }
}
