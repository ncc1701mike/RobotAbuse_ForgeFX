using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MockInputHandler- mock class that simulates user input for testing
public class MockInputHandler : IInputHandler
{
    // Dictionary to store the simulated states of each key
    private Dictionary<KeyCode, bool> keyStates = new Dictionary<KeyCode, bool>();

    
    // SetKey method simulates pressing or releasing a key
    public void SetKey(KeyCode key, bool pressed)
    {
        keyStates[key] = pressed;
    }

    // GetKey method checks if a key is pressed - returns true if the key is pressed, false otherwise
    public bool GetKey(KeyCode key)
    {
        return keyStates.ContainsKey(key) && keyStates[key];
    }
}
