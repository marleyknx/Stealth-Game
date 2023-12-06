using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]  float floorOffsetY;
    [SerializeField] float currentSpeed;
    [SerializeField] float rotateSpeed;

    Rigidbody rb;
    Vector3 move;
    Vector3 Direction;
    float inputAmount;
    Vector3 raycastFloorPos;
    Vector3 floorMovement;
    Vector3 gravity;
    Vector3 CombinedRaycast;



    PlayerAnimation playerAnimation;

   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<PlayerAnimation>();  
      
    }
   


        public void SetMoveDirection(Vector2 direction)
    {
      
        // reset movement
        Direction = Vector3.zero;

        // get vertical and horizontal movement input (controller and WASD/ Arrow Keys)
        move = new Vector3(direction.x, 0, direction.y);


        // base movement on camera
        Vector3 correctedVertical = move.z * Camera.main.transform.forward;
        Vector3 correctedHorizontal = move.x * Camera.main.transform.right;
        Vector3 combinedInput = correctedHorizontal + correctedVertical;


        // normalize so diagonal movement isnt twice as fast, clear the Y so your character doesnt try to
        // walk into the floor/ sky when your camera isn't level
        Direction = new Vector3((combinedInput).normalized.x, 0, (combinedInput).normalized.z);
        // make sure the input doesnt go negative or above 1;
        float InputMagnitude = Mathf.Abs(move.x) + Mathf.Abs(move.z);
        inputAmount = Mathf.Clamp01(InputMagnitude);

        // rotate player to movement direction
        Quaternion rot = Quaternion.LookRotation(Direction);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * inputAmount * rotateSpeed);
        transform.rotation = targetRotation;



        playerAnimation.SetWalk(inputAmount);

    }






    private void FixedUpdate()
    {
        // if not grounded , increase down force
        if (FloorRaycasts(0, 0, 0.6f) == Vector3.zero)
        {
            gravity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        }


        // actual movement of the rigidbody + extra down force
        rb.velocity = (Direction * currentSpeed * inputAmount) + gravity;



        // find the Y position via raycasts
        floorMovement = new Vector3(rb.position.x, FindFloor().y + floorOffsetY, rb.position.z);

        // only stick to floor when grounded
        if (FloorRaycasts(0, 0, 0.6f) != Vector3.zero && floorMovement != rb.position)
        {
            // move the rigidbody to the floor
            rb.MovePosition(floorMovement);
            gravity.y = 0;
        }
    }



    Vector3 FindFloor()
    {
        // width of raycasts around the centre of your character
        float raycastWidth = 0.25f;
        // check floor on 5 raycasts   , get the average when not Vector3.zero  
        int floorAverage = 1;

        CombinedRaycast = FloorRaycasts(0, 0, 1.6f);
        floorAverage += (getFloorAverage(raycastWidth, 0) + getFloorAverage(-raycastWidth, 0) + getFloorAverage(0, raycastWidth) + getFloorAverage(0, -raycastWidth));

        return CombinedRaycast / floorAverage;
    }


    // only add to average floor position if its not Vector3.zero
    int getFloorAverage(float offsetx, float offsetz)
    {

        if (FloorRaycasts(offsetx, offsetz, 1.6f) != Vector3.zero)
        {
            CombinedRaycast += FloorRaycasts(offsetx, offsetz, 1.6f);
            return 1;
        }
        else { return 0; }
    }

    Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
    {
        RaycastHit hit;
        // move raycast
        raycastFloorPos = transform.TransformPoint(0 + offsetx, 0 + 0.5f, 0 + offsetz);

        Debug.DrawRay(raycastFloorPos, Vector3.down, Color.magenta);
        if (Physics.Raycast(raycastFloorPos, -Vector3.up, out hit, raycastLength))
        {
            return hit.point;
            
        }
        else return Vector3.zero;
    }
}
