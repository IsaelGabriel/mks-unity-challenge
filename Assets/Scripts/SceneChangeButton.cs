using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  buttons that send a "ChangeScene" signal to the current GameManager
/// </summary>
public class SceneChangeButton : SignalHandler
{
    void Start()
    {
        AddListener(GameManager.INSTANCE); // Add the current GameManager to Listener list
    }

    public void ButtonClicked(string sceneID) // Call when the button is clicked, sends "ChangeScene" signal
    {
        SendSignal($"ChangeScene: {sceneID}"); // Send "ChangeScene" signal to GameManager with desired scene's name
    }

}
