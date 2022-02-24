using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIDontDestroyOnLoad : MonoBehaviour
{
    public string scene;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(scene);
    }
}
