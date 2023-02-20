using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : SignalHandler
{
    public static GameManager INSTANCE;
    public static float MatchTime  = 60f, EnemySpawnTime = 5f;


    private int _score = 0, _highScore = 0;
    private TextMeshProUGUI _scoreTextObject;
    private float _matchTimeCount = 0f;
    private bool _inMatch = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Awake() {
        if(INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
        DontDestroyOnLoad(INSTANCE);
    }

    void Update()
    {
        if(!_inMatch) return;
        _matchTimeCount += Time.deltaTime;

        float matchTimeRemaining = MatchTime - _matchTimeCount;
        string matchTimeMinutes = (matchTimeRemaining/60f).ToString().Split(',')[0];
        matchTimeRemaining -= int.Parse(matchTimeMinutes) * 60f;
        string matchTimeRemainingString = matchTimeRemaining.ToString("F2").Replace(',','.');
        if(matchTimeRemainingString.Split(".")[0].Length < 2) matchTimeRemainingString = "0" + matchTimeRemainingString;
        _scoreTextObject.text = $"Pontos: {_score}<br>Maior Pontuação: {_highScore}<br>{matchTimeMinutes}.{matchTimeRemainingString}";


        if(_matchTimeCount >= MatchTime) SceneManager.LoadScene("MatchEnd");
    }

    public override void ReceiveSignal(string signal)
    {
        switch(signal)
        {
            case "EnemyDead":
                _score += 1;
                if(_score > _highScore) _highScore = _score;
            break;
            default:
                if(signal.Contains("ChangeScene:"))
                {
                    if(signal.Split()[1] == "RevertSettings")
                    {
                        PlayerPrefs.SetFloat("MatchTime", 60f);
                        PlayerPrefs.SetFloat("SpawnTime", 5f);
                        SceneManager.LoadScene("MainMenu");
                        return;
                    }
                    if(SceneManager.GetActiveScene().name == "Settings")
                    {
                        PlayerPrefs.SetFloat("MatchTime", GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value);
                        PlayerPrefs.SetFloat("SpawnTime", GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value);
                    }

                    SceneManager.LoadScene(signal.Split()[1]);
                }

            break;
        }
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _matchTimeCount = 0f;
        MatchTime = PlayerPrefs.GetFloat("MatchTime", MatchTime);
        EnemySpawnTime = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime);
        _inMatch = (scene.name == "SampleScene");
        _highScore = PlayerPrefs.GetInt("HighScore",0);


        if(scene.name == "SampleScene")
        {
            _scoreTextObject = GameObject.Find("Canvas/UI Text").GetComponent<TextMeshProUGUI>();
            _scoreTextObject.text = $"Pontos: {_score}";
        }else if(scene.name == "Settings")
        {
            GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MatchTime", MatchTime);
            GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime);
        }else if(scene.name == "MatchEnd")
        {
            if(_score > PlayerPrefs.GetInt("HighScore",0)) PlayerPrefs.SetInt("HighScore", _score);
            GameObject.Find("Canvas/Score Text").GetComponent<TextMeshProUGUI>().text = $"Pontos: {_score}<br>Maior pontuação: {_highScore}";
        }

    }
}
