using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Tooltip("la sensibilitée de la camera")]
    [Range(0f, 2f)]
    public float xSensiblity, ySensiblity;
    public CinemachineVirtualCamera virtualCam;
    public Transform pointToFollow;

    [Space]
    public PlayerInputHandler playerInputHandler;
    public FrameInput frameInput;
    // Start is called before the first frame update
    void Start()
    {
        /* var pov = vcam.AddCinemachineComponent<CinemachinePOV>();
         pov.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
         pov.m_HorizontalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;*/
        var pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
        pov.m_VerticalAxis.m_MaxSpeed = 0;
        pov.m_HorizontalAxis.m_MaxSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        frameInput = playerInputHandler.frameInput;
        var pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
        Vector2 lookDirecction = frameInput.Aim;
        Vector3 look = new Vector3(lookDirecction.x, 0, lookDirecction.y);
        look.x = pov.m_HorizontalAxis.m_InputAxisValue;
        pov.m_VerticalAxis.m_MaxSpeed = ySensiblity;
        pov.m_HorizontalAxis.m_MaxSpeed = xSensiblity;


        pov.m_VerticalAxis.Update(Time.deltaTime);
        pov.m_HorizontalAxis.Update(Time.deltaTime);


    }


    private void LateUpdate()
    {
        var pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
      //  pointToFollow.localEulerAngles = new Vector3(pov.m_VerticalAxis.Value, pointToFollow.localEulerAngles.y, pointToFollow.localEulerAngles.z);
      //  player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, pov.m_HorizontalAxis.Value, player.transform.eulerAngles.z);
    }
}
