using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This serves as a base for most classes in the game, it handles interactions between objects in a way that they don't deppend on each other.
/// </summary>
public class SignalHandler : MonoBehaviour
{
    [SerializeField] private List<SignalHandler> listeners; // List of listeners of this object.
    
    public virtual void ReceiveSignal(string signal){} // Unique for each child.

    protected void SendSignal(string signal) // Sends a signal to all of its listeners
    {
        for(int i = 0; i < listeners.Count; i++) listeners[i].ReceiveSignal(signal);
    }

    public void AddListener(SignalHandler sh) // Add SignalHandler to Listener List
    {
        listeners.Add(sh);
    }

    public void RemoveListener(SignalHandler sh) // Remove SignalHandler from Listener List
    {
        listeners.Remove(sh);
    }
}
