
using UnityEngine;

// UnityInputHandler - class that handles real user input from Unity's input system
public class UnityInputHandler : IInputHandler
{
    // GetKey method checks if a key is pressed - returns true if the key is pressed, false otherwise
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
}

