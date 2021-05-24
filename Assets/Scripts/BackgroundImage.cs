using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    [Range(0,1)][SerializeField] private float scrollSpeed = 0.05f;
    private Renderer renderer;
    private float BackgroundScrollingXoffSet = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        if (BackgroundScrollingXoffSet < 1)
        {
            BackgroundScrollingXoffSet += scrollSpeed * Time.deltaTime;
        }
        else
        {
            BackgroundScrollingXoffSet = 0;
        }

        Vector2 newOffSet = new Vector2(BackgroundScrollingXoffSet,0.0f);         
        renderer.material.mainTextureOffset= newOffSet;
    }
}
