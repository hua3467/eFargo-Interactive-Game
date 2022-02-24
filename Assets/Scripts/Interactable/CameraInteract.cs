
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
    private Camera _cam;
    private List<IClickable> _clickedObjects = new List<IClickable>();
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider)
            {
                IClickable clicked = hit.transform.gameObject.GetComponent<IClickable>();
                clicked.Clicked();
                _clickedObjects.Add(clicked);
            }
            else
            {
                foreach (IClickable clickedObject in _clickedObjects)
                {
                    clickedObject.UnClicked();
                }

                _clickedObjects.Clear();
            }
        }
    }
}
