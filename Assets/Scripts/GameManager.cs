using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SignalHandler
{
    public static GameManager INSTANCE;
    
    void Awake() {
        if(INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
        DontDestroyOnLoad(INSTANCE);
    }
}
