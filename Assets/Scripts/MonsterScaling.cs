using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MonsterScaling : MonoBehaviour
{
    public float minScale = .5f;
    
    private GameObject[] _monsters;
    private GameObject _background;

    // Start is called before the first frame update
    void Start()
    {
        _monsters = GameObject.FindGameObjectsWithTag("Enemies");
        _background = GameObject.FindGameObjectWithTag("Background");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in _monsters)
        {
            var backgroundPosition = _background.transform.position;
            if (obj.transform.position.x < backgroundPosition.x)
            {
                obj.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                obj.GetComponent<SpriteRenderer>().flipX = false;
            }
            var distanceToCenter = Vector3.Distance(obj.transform.position, backgroundPosition);
            var scaleRatio = Math.Max(minScale, distanceToCenter / 
                                                  (_background.GetComponent<SpriteRenderer>().size.x / 2));
            obj.transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
        }
    }
}
