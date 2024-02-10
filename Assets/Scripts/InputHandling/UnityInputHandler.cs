
using UnityEngine;

public class UnityInputHandler : IInputHandler
{
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
}

