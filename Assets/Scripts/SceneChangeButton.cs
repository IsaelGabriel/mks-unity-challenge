using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : SignalHandler
{

    void Start()
    {
        AddListener(GameManager.INSTANCE);
    }

    public void ButtonClicked(string sceneID)
    {
        SendSignal($"ChangeScene: {sceneID}");
    }

}
