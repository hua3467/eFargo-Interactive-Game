using System;
using System.Collections.Generic;
using Interactable.Draggable;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChallengeController : MonoBehaviour
{
    public GameObject dialogPrefab;
    public GameObject dialogueCanvas;
    public GameObject confirmButton;
    
    public Challenge Challenge;

    private GameObject _dialog;
    private readonly  List<SnapPoint> _tagPoints = new List<SnapPoint>(); 

    private void Start()
    {
        
        Challenge = new Challenge(new List<string> { "Some text", "Some text 2", "TAG" }, "Light", "Bulb");
        
        var tagPointsObjects = GameObject.FindGameObjectsWithTag(Challenge.TagType);
        foreach (var obj in tagPointsObjects)
        {
            _tagPoints.Add(obj.GetComponent<SnapPoint>());
        }
        
        if (Challenge != null)
        {
            MakeDialog(Challenge.Dialog.Dequeue(), Next);
        }
    }

    public void FinishTagging()
    {
        confirmButton.SetActive(false);
        foreach (var tagPoint in _tagPoints)
        {
            if (tagPoint.AttachedGameObject == null || tagPoint.AttachedGameObject.name != Challenge.MonsterName)
            {
                MakeDialog("Oops! The tags are wrong, please try again", StartTagging);
                return;
            }
        }
        Next();
    }

    private void Next()
    {
        Destroy(_dialog);
        var option = Challenge.Dialog.Dequeue();
        if (option.Equals("TAG"))
        {
            StartTagging();
        }
        else
        {
            MakeDialog(option, Next);
        }
    }

    private void MakeDialog(string text, UnityAction buttonCallBack)
    {
        _dialog = Instantiate(dialogPrefab, dialogPrefab.transform.position, Quaternion.identity);
        _dialog.transform.parent = dialogueCanvas.transform;
        _dialog.transform.GetChild(1).GetComponent<Image>().sprite = Challenge.Monster;
        _dialog.GetComponentInChildren<TextMeshProUGUI>().text = text;
        _dialog.GetComponentInChildren<Button>().onClick.AddListener(buttonCallBack);
    }

    private void StartTagging()
    {
        Destroy(_dialog);
        confirmButton.SetActive(true);
    }
}