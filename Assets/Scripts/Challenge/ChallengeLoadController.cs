using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeLoadController : MonoBehaviour, ISingleton
{
    public GameObject challengeButtonPrefab;
    public GameObject challengeMenuContainer;
    public Transform challengeMenuTop;

    private readonly List<Challenge> _challenges = new();
    private Challenge _runChallenge;
    
    private static ChallengeLoadController Instance { get; set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
        } 
        else 
        { 
            Instance = this; 
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name != "SchoolView")
        {
            return;
        }
        
        SceneManager.sceneLoaded += RunChallenge;
        
        // TODO: Load from DB here
        _challenges.Add(new Challenge(new List<string> { "Let's play light black out hour", "Drag and drop me on any artificial light source", "TAG", "Describe the tag, how would you solve the issue", "<Journaling here>", "Tag successfully created" }, "Light", "Bulb", 250, "RoomView"));
        
        var offset = 0f;
        for(int i = 0; i < _challenges.Count; i++)
        {
            var challengeMenuTopOffset = challengeMenuTop.position + new Vector3(0, offset, 0);
            offset += 4f;
            var challengeButton = Instantiate(challengeButtonPrefab, challengeMenuTopOffset, Quaternion.identity);
            challengeButton.transform.parent = challengeMenuContainer.transform;
            challengeButton.transform.localScale = challengeMenuTop.localScale;
            challengeButton.SetActive(true);
            var buttonScript = challengeButton.GetComponentInChildren<ChallengeButton>();
            buttonScript.loadController = this;
            buttonScript.completedCheckMark.SetActive(_challenges[i].Completed);
            buttonScript.Index = i;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadSceneChallenge(int index)
    {
        Debug.Log("Running challenge...");
        _runChallenge = _challenges[index];
        SceneManager.LoadScene(_runChallenge.SceneName);
    }

    public void RunChallenge(Scene scene, LoadSceneMode loadSceneMode)
    {
        var challengeController = GameObject.FindGameObjectWithTag("ChallengeController").GetComponent<ChallengeController>();
        challengeController.challenge = _runChallenge;
    }
}
