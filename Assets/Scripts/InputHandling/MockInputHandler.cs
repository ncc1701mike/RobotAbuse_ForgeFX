using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MockInputHandler- mock class that simulates user input for testing
public class MockInputHandler : IInputHandler
{
    private Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();

    public void SetKey(KeyCode key, bool pressed)
    {
        keyStates[key] = pressed;
    }

    public bool GetKey(KeyCode key)
    {
        return keyStates.ContainsKey(key) && keyStates[key];
    }
}
