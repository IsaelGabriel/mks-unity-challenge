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


    private int _score = 0;
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
        if(_matchTimeCount >= MatchTime) SceneManager.LoadScene("MatchEnd");
    }

    public override void ReceiveSignal(string signal)
    {
        switch(signal)
        {
            case "EnemyDead":
                _score += 1;
                _scoreTextObject.text = $"Pontos: {_score}";
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


        if(scene.name == "SampleScene")
        {
            _scoreTextObject = GameObject.Find("Canvas/Score Text").GetComponent<TextMeshProUGUI>();
            _scoreTextObject.text = $"Pontos: {_score}";
        }else if(scene.name == "Settings")
        {
            GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MatchTime", MatchTime);
            GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime);
        }else if(scene.name == "MatchEnd")
        {
            if(_score > PlayerPrefs.GetInt("HighScore",0)) PlayerPrefs.SetInt("HighScore", _score);
            GameObject.Find("Canvas/Score Text").GetComponent<TextMeshProUGUI>().text = $"Pontos: {_score}<br>Maior pontuação: {PlayerPrefs.GetInt("HighScore",0)}";
        }

    }
}
