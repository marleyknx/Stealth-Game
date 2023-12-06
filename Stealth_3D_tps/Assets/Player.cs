using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isCrouch;

    PlayerMovement playerMovement;
    PlayerInputHandler playerInputHandler;
    FrameInput frameInput;


    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameInput = playerInputHandler.frameInput;

        SetDirecction(frameInput.move);




    }


    public void SetDirecction(Vector2 dir) => playerMovement.SetMoveDirection(dir);

}
