using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChallengeButton : MonoBehaviour
{
    public ChallengeLoadController loadController;

    [NonSerialized] public int Index;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        loadController.LoadSceneChallenge(Index);
    }
}
