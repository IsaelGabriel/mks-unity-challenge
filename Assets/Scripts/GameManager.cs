using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SignalHandler
{
    public static GameManager INSTANCE;
    
    private int _score = 0;


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
                Debug.Log(_score);
            break;
        }
    }
}
