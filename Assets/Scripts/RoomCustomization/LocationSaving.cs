using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSaving : MonoBehaviour
{
    private Vector3 _location;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _location = gameObject.transform.position;
    }
}
