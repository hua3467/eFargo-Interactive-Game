using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeUILoadController : MonoBehaviour
{
    public GameObject challengeButtonPrefab;
    public GameObject challengeMenuContainer;
    public Transform challengeMenuTop;
    public float offset;

    public ChallengeLoadController loadController;
    public List<Challenge> challenges;

    public void CreateChallengeListUI()
    {
        float offsetTotal = 0;
        for(var i = 0; i < challenges.Count; i++, offsetTotal += offset)
        {
            var challengeMenuTopOffset = challengeMenuTop.position + new Vector3(0, offsetTotal, 0);
            
            var challengeButton = Instantiate(challengeButtonPrefab, challengeMenuTopOffset, Quaternion.identity);
            challengeButton.transform.parent = challengeMenuContainer.transform;
            challengeButton.transform.localScale = challengeMenuTop.localScale;
            challengeButton.SetActive(true);
            
            challengeButton.GetComponentInChildren<TextMeshProUGUI>().text = challenges[i].ChallengeName;
            
            var buttonScript = challengeButton.GetComponentInChildren<ChallengeButton>();
            buttonScript.loadController = loadController;
            buttonScript.completedCheckMark.SetActive(challenges[i].Completed);
            buttonScript.Index = i;

            var buttonObject = challengeButton.GetComponent<Button>();
            buttonObject.interactable = !challenges[i].Completed;
        }
    }
}
