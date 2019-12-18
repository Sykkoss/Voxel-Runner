using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        SceneManager.sceneUnloaded += RedrawCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RedrawCanvas<Scene> (Scene scene)
    {
        if (canvas)
            canvas.SetActive(true);
    }
}
