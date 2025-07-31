using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
    }

    public Vector2 GetVector2Normalized()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>().normalized;
    }
    
    public bool Jumped()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public Vector2 GetMouseAxis()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
