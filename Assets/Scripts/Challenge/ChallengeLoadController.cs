using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeLoadController : MonoBehaviour, ISingleton
{
    public List<Sprite> monsterSprites;

    public List<Challenge> _challenges = new();
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
        SceneManager.sceneLoaded += RunChallenge;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name != "SchoolView")
        {
            return;
        }
        
        CreateChallengeUI();
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        // TODO: Load from DB here
        _challenges.Add(new Challenge("Light Blackout Hour", new[] { "Let's play light black out hour", "Drag and drop me on all the artificial light sources", "<TAG>", "<JOURNAL>/Awesome, what could you do to reduce the energy usage of artificial lights?", "Tag successfully created", "Let's try turning off the lights, click on the light switch and open the blinds to let natural light in", "Awesome! Enjoy the sunshine!" }, "Light", "Bulb", 250, "classroom 1"));
        _challenges.Add(new Challenge("Daily Hot and Cold",new[] {"Let's play Daily Hot and Cold!", "Drag and drop me on all the heating or cooling devices", "<TAG>", "<JOURNAL>/How should the classroom's thermostat be set so the least energy is used?", "Tag successfully created", "Double check to see the thermostat is set at an appropriate temperature", "Awesome! You've completed the challenge!"}, "Temp", "AC", 250, "classroom 1"));
        foreach(var challenge in _challenges)
        {
            foreach (var sprite in monsterSprites)
            {
                if (challenge.MonsterName.ToLower().Equals(sprite.name.ToLower()))
                {
                    challenge.Monster = sprite;
                    break;
                }
            }
        }
        CreateChallengeUI();
    }

    private void CreateChallengeUI()
    {
        var uiLoadController = GameObject.Find("ChallengeUILoadController").GetComponent<ChallengeUILoadController>();
        uiLoadController.challenges = _challenges;
        uiLoadController.loadController = this;
        
        uiLoadController.CreateChallengeListUI();
    }

    public void LoadSceneChallenge(int index)
    {
        var output = "";
        foreach (var str in _challenges[index].Dialog)
        {
            output += str;
        }
        Debug.Log(output);
        Debug.Log("Running challenge...");
        _runChallenge = _challenges[index];
        SceneManager.LoadScene(_runChallenge.SceneName);
    }

    private void RunChallenge(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "SchoolView")
        {
            return;
        }
        var challengeController = GameObject.FindGameObjectWithTag("ChallengeController").GetComponent<ChallengeController>();
        challengeController.challenge = _runChallenge;
        challengeController.InitiateChallenge();
    }
}
