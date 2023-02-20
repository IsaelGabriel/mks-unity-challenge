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
        MatchTime = PlayerPrefs.GetFloat("MatchTime", MatchTime);
        EnemySpawnTime = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime);

        if(scene.name == "SampleScene")
        {
            _scoreTextObject = GameObject.Find("Canvas/Score Text").GetComponent<TextMeshProUGUI>();
            _scoreTextObject.text = $"Pontos: {_score}";
        }else if(scene.name == "Settings")
        {
            GameObject.Find("Canvas/Match Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("MatchTime", MatchTime);
            GameObject.Find("Canvas/Enemy Spawn Time/Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SpawnTime", EnemySpawnTime);
        }

    }
}
