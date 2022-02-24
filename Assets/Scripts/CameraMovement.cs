using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float max_x = 8f;
    public float min_x = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) && transform.position.x <= max_x)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= min_x)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("SchoolView");
        }
    }
}
