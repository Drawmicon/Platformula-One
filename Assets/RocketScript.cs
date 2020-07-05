using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public Rigidbody player;

    bool isInAir;

    float speedDamp;
    float turnAngle;
    float stationaryTurnSpeed;
    float movingTurnSpeed;
    float forwardAmount;
    // 
    float h;
    float v;

    Vector3 camForward;
    Vector3 camRight;
    Vector3 movement;

    Vector3 groundNormal;

    Camera playerCamera;

    private void Start()
    {
        isInAir = false;
        groundNormal = new Vector3(0,1,0);
    }

    void FixedUpdate()
    {
        

        Movement();

        if ((Input.GetAxis("Vertical") > 0))
        {
            player.velocity = movement;
        }
    }

    /////////////////////////////////////////////CHARACTER MOVEMENT/////////////////////////////////////////
    void Movement()
    {
        //Only handles upright locomotion
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        //This is all the vector directions from the camera
        camForward = playerCamera.transform.forward;
        camRight = playerCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        //This is where the magic happens
        movement = v * camForward + h * camRight;

        movement = transform.InverseTransformDirection(movement);
        movement = Vector3.ProjectOnPlane(movement, groundNormal);


        //Sprint system below
        //isInAir is a boolean for the grounded logic
        if (!isInAir && Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2f;
            speedDamp = 1f;
            movement.z = Mathf.Clamp(movement.z, -1f, 1f);
            turnAngle = Mathf.Clamp(turnAngle, -1f, 1f);
        }
        else
        {
            movement *= 0.5f;
            speedDamp = 0.2f;
            movement.z = Mathf.Clamp(movement.z, -.5f, .5f);
            turnAngle = Mathf.Clamp(turnAngle, -1f, 1f);
        }

        turnAngle = Mathf.Atan2(movement.x, movement.z);

        //This is for extra rotation controllable from variables above
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAngle * turnSpeed * Time.deltaTime, 0);

        //Finally, set all animator parameters
        //turnAngle is the float angle created far up above
        //That value will drive the turning animation/blend trees for turning
        //the character.
        //anim.SetFloat("Forward", movement.z, speedDamp, Time.deltaTime);
        //anim.SetFloat("Turn", turnAngle, turnDamp, Time.deltaTime);
    }

}
