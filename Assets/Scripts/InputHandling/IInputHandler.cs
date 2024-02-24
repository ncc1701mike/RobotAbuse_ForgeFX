using UnityEngine;

// Input Interface for input testing - provides a common interface for real and simulated input handlers
public interface IInputHandler
{
    bool GetKey(KeyCode key);  // GetKey method to be implemented by the input handler
}