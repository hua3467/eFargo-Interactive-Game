using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using Interactable.Draggable;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChallengeController : MonoBehaviour
{
    public GameObject dialogPrefab;
    public GameObject dialogCanvas;
    public GameObject journalDialog;
    public Transform dialogTransform;
    public Transform journalDialogTransform;
    public GameObject confirmButton;
    
    public Challenge challenge;

    private GameObject _dialog;
    private readonly List<SnapPoint> _tagPoints = new();

    public void InitiateChallenge()
    {
        var output = "";
        foreach (var str in challenge.Dialog)
        {
            output += str;
        }
        Debug.Log(output);
        var tagPointsObjects = GameObject.FindGameObjectsWithTag(challenge.TagType);
        foreach (var obj in tagPointsObjects)
        {
            _tagPoints.Add(obj.GetComponent<SnapPoint>());
        }
        MakeDialog(dialogPrefab, challenge.Dialog.Dequeue(), Next, dialogTransform);
    }

    public void FinishTagging()
    {
        confirmButton.SetActive(false);
        foreach (var tagPoint in _tagPoints)
        {
            if (tagPoint.AttachedGameObject == null || tagPoint.AttachedGameObject.name != challenge.MonsterName)
            {
                MakeDialog(dialogPrefab, "Oops! The tags are wrong, please try again", StartTagging, dialogTransform);
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
            Debug.Log("Challenge completed");
            return;
        }
        // split flag value from text here
        var option = challenge.Dialog.Dequeue().Split('/');
        switch (option[0])
        {
            case "<TAG>":
                StartTagging();
                break;
            case "<JOURNAL>":
                MakeDialog(journalDialog, option[1], Next, journalDialogTransform);
                break;
            default:
                MakeDialog(dialogPrefab, option[0], Next, dialogTransform);
                break;
        }
    }

    private void MakeDialog(GameObject dialogBox, string text, UnityAction buttonCallBack, Transform location)
    {
        Debug.Log(buttonCallBack.Method);
        _dialog = Instantiate(dialogBox, location.position, Quaternion.identity);
        _dialog.transform.parent = dialogCanvas.transform;
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
        // TODO: reward carbon coins here
        challenge.Completed = true;
    }
}