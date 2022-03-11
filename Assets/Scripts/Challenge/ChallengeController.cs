using System.Collections.Generic;
using Interactable.Draggable;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChallengeController : MonoBehaviour
{
    public GameObject dialogPrefab;
    public GameObject dialogueCanvas;
    public GameObject confirmButton;
    
    [FormerlySerializedAs("Challenge")] 
    public Challenge challenge;

    private GameObject _dialog;
    private readonly  List<SnapPoint> _tagPoints = new List<SnapPoint>(); 

    private void Start()
    {
        
        //challenge = new Challenge(new List<string> { "Let's play light black out hour", "Drag and drop me on any artificial light source", "TAG", "Describe the tag, how would you solve the issue", "<Journaling here>", "Tag successfully created" }, "Light", "Bulb", 250);

        var tagPointsObjects = GameObject.FindGameObjectsWithTag(challenge.TagType);
        foreach (var obj in tagPointsObjects)
        {
            _tagPoints.Add(obj.GetComponent<SnapPoint>());
        }
        
        if (challenge != null)
        {
            MakeDialog(challenge.Dialog.Dequeue(), Next);
        }
    }

    public void FinishTagging()
    {
        confirmButton.SetActive(false);
        foreach (var tagPoint in _tagPoints)
        {
            if (tagPoint.AttachedGameObject == null || tagPoint.AttachedGameObject.name != challenge.MonsterName)
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
        if (challenge.Dialog.Count == 0)
        {
            ChallengeComplete();
            return;
        }
        var option = challenge.Dialog.Dequeue();
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
        _dialog.transform.GetChild(1).GetComponent<Image>().sprite = challenge.Monster;
        _dialog.GetComponentInChildren<TextMeshProUGUI>().text = text;
        _dialog.GetComponentInChildren<Button>().onClick.AddListener(buttonCallBack);
    }

    private void StartTagging()
    {
        Destroy(_dialog);
        confirmButton.SetActive(true);
    }

    private void ChallengeComplete()
    {
        // TODO: increment carbon coins for user here
    }
}