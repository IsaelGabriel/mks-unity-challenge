using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// GameManager behaviour, handles all game interactions (saving variables, changing scenes)
/// <summary>
public class GameManager : SignalHandler
{
    // Static variables
    public static GameManager INSTANCE; // Current GameManager
    public static float MatchTime  = 60f, EnemySpawnTime = 5f; // Variables for match duration and time between enemy spawns

    // Public variables
    private int _score = 0, _highScore = 0; // Current score and high score
    private TextMeshProUGUI _UITextObject; // On match UI text object
    private float _matchTimeCount = 0f; // Counts the match time
    private bool _inMatch = false; // Made for checking if match is running

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Call OnSceneLoaded method when SceneManager loads a scene
    }

    void Awake()
    {
        if(INSTANCE != null && INSTANCE != this) // If there is already a GameManager.INSTANCE and it isn't this object, destroy it
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this; // If there is not a GameManager.INSTANCE, make this the instance
        DontDestroyOnLoad(INSTANCE); // GameManager.INSTANCE doesn't get destroyed when loading scenes
    }

    void Update()
    {
        if(!_inMatch) return; // Doesn't run the following Update code if not in a match
        _matchTimeCount -= Time.deltaTime; // Decrease count for the match time

        string matchTimeMinutes = (matchTimeCount/60f).ToString().Split(',')[0]; // Get remaining minutes
        string matchTimeRemaining = (matchTimeCount - (int.Parse(matchTimeMinutes) * 60f)).ToString("F2").Replace(',','.'); // Gets remaining seconds, turns it into a string formatted to only 2 decimal places, then turns the ',' into a '.' 
        if(matchTimeRemaining.Split(".")[0].Length < 2) matchTimeRemaining = "0" + matchTimeRemaining; // If remaining seconds string doesnt have 2 numbers before the '.', make add a '0' to the left
        _UITextObject.text = $"Pontos: {_score}<br>Maior Pontuação: {_highScore}<br>{matchTimeMinutes}.{matchTimeRemaining}"; // Display score, high score, and remaining time ("#.##.##")

        if(_matchTimeCount <= 0f) SceneManager.LoadScene("MatchEnd"); // If _matchTimeCount ended, change to MatchEnd scene
    }

    public override void ReceiveSignal(string signal) // Interpets signals received
    {
        switch(signal)
        {
            case "EnemyDead":
                _score++; // Increase current score
                if(_score > _highScore) _highScore = _score; // If score surpasses high score, update it as well
            break;
            default:
                if(signal.Contains("RevertSettings")) //  If signal contains the "RevertSettings" command
                {
                    PlayerPrefs.SetFloat("MatchTime", 60f); // Set MatchTime to default
                    PlayerPrefs.SetFloat("SpawnTime", 5f); // Set SpawnTime to default
                    SceneManager.LoadScene("MainMenu"); // Go to main menu
                    return;
                }else if(signal.Contains("ChangeScene:")) // If signal contains the "ChanceScene" command
                {
                    if(SceneManager.GetActiveScene().name == "Settings") // If in "Settings" scene, save settings
                    {
                        PlayerPrefs.SetFloat("MatchTime", GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value);
                        PlayerPrefs.SetFloat("SpawnTime", GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value);
                    }

                    SceneManager.LoadScene(signal.Split()[1]); // Load scene specified in the "ChangeScene" command
                }

            break;
        }
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode) // Is called by the SceneManager when a scene is loaded
    {
        if(_score > PlayerPrefs.GetInt("HighScore",0)) PlayerPrefs.SetInt("HighScore", _score); // if current score surpasses saved high score, save it
        MatchTime = PlayerPrefs.GetFloat("MatchTime", MatchTime); // Get "MatchTime" PlayerPref, if nonexistent, mantain value
        EnemySpawnTime = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime); // Get "SpawnTime" PlayerPref, if nonexistent, mantain value
        _inMatch = (scene.name == "SampleScene"); // If scene loaded is the default game scene, start match
        _highScore = PlayerPrefs.GetInt("HighScore",_highScore); // Get saved high score
        _matchTimeCount = MatchTime; // Reset _matchTimeCount


        if(scene.name == "SampleScene") // If default game scene
        {
            _UITextObject = GameObject.Find("Canvas/UI Text").GetComponent<TextMeshProUGUI>(); // Get _UITextObject
        }else if(scene.name == "Settings")
        {
            GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MatchTime", MatchTime); // Set Match Time slider to saved "MatchTime" value
            GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime); // Set Enemy Spawn Time slider to saved "SpawnTime" value
        }else if(scene.name == "MatchEnd")
        {
            GameObject.Find("Canvas/Score Text").GetComponent<TextMeshProUGUI>().text = $"Pontos: {_score}<br>Maior pontuação: {_highScore}"; // Set MatchEnd's score text
        }

    }
}
