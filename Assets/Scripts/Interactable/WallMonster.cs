using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Sprite monsterSprite;
    public GameObject backgroundObject;
    
    private Vector3 _offset;
    private Camera _mainCamera;
    private Plane _dragPlane;
    private GameObject _monster;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        Vector3 position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        // create the monster at current mouse position
        _monster = Instantiate(monsterPrefab, position, Quaternion.identity);
        _monster.GetComponent<SpriteRenderer>().sprite = monsterSprite;
        var monsterPosition = _monster.transform.position;
        
        _dragPlane = new Plane(_mainCamera.transform.forward, monsterPosition);
        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _offset = monsterPosition - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {
        Ray camRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        _dragPlane.Raycast(camRay, out planeDist);
        _monster.transform.position = camRay.GetPoint(planeDist) + _offset;
    }

    private void OnMouseUp()
    {
        var monsterCollider = _monster.GetComponent<PolygonCollider2D>();
        var backgroundCollider = backgroundObject.GetComponent<PolygonCollider2D>();
        if (!monsterCollider.bounds.Intersects(backgroundCollider.bounds))
        {
            Destroy(_monster);
        }
    }
}
