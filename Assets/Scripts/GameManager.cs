using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : SignalHandler
{
    public static GameManager INSTANCE;
    
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
                    SceneManager.LoadScene(signal.Split()[1]);
                }

            break;
        }
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "SampleScene")
        {
            _scoreTextObject = GameObject.Find("Canvas").transform.Find("Score Text").GetComponent<TextMeshProUGUI>();
            _scoreTextObject.text = $"Pontos: {_score}";
        }

    }
}
