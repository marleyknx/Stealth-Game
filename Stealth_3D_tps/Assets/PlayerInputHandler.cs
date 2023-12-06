using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput _playerInput;
    private InputAction moveAction, AimAction,crouchAction;
    public FrameInput frameInput;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions["Walk"];
        AimAction = _playerInput.actions["Aim"];
        crouchAction = _playerInput.actions["Crouch"];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameInput = gatherInput(); 
    }



    public FrameInput gatherInput()
    {
        return new FrameInput
        {
            move = moveAction.ReadValue<Vector2>(),
            Aim = moveAction.ReadValue<Vector2>(),
            Crouch = crouchAction.WasPressedThisFrame(),

        };
    }

}


public struct FrameInput
{
   public Vector2 move;
    public Vector2 Aim;
    public bool Crouch;
}