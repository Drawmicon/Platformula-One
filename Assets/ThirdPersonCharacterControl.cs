using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    private Rigidbody player;
    private Vector3 input;
    public float speed;
    public float camDelay;

    public float acceleration;
    public float accelerationRate;
    public float accelerationRateLimit;
    private Vector3 jump;

    private bool isGrounded;// true if on ground, if on ground, then can jump
    public float jumpHeight;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        camDelay = 0.9f;
        jump = new Vector3(0f,jumpHeight,0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            player.AddForce(jump, ForceMode.Impulse);
            isGrounded = false;
        }

        /*if (Input.GetKey("space"))
        {
            // jump
            player.AddForce(new Vector3((acceleration*jumpHeight)+ jumpHeight * Time.fixedDeltaTime, 0, 0));
        }*/

        if (Input.GetKey("left shift") && (Input.GetAxis("Vertical")* Input.GetAxis("Vertical") == 1 || Input.GetAxis("Horizontal")* Input.GetAxis("Horizontal") == 1) && acceleration <= accelerationRateLimit)
        {
            //accelerate
            acceleration += accelerationRate;
        }
        else
        {
            if (acceleration >= 1.5)
            {
                acceleration -= accelerationRate;
            }
            else
            {
                acceleration = 1;
            }
        }
        //*****************************************************
        transform.LookAt(transform.position + new Vector3(input.x * camDelay, 0f, input.z * camDelay));
        input = new Vector3(Input.GetAxis("Horizontal")*speed, player.velocity.y, Input.GetAxis("Vertical") * speed);//https://www.youtube.com/watch?v=4Qq7d9elXNA
    }

    void FixedUpdate()
    {
        player.velocity = input*acceleration;
    }

    /*
    //[SerializeField]
    private Rigidbody player;

    //public float turning;
    public float speed;
    public float acceleration;
    public float accelerationRate;
    public float accelerationRateLimit;

    public float jumpHeight;
    public float runningJumpHeight;

    private Vector3 vec;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey("left shift") && Input.GetKey("space"))
        {
            //high jump
            player.AddForce(new Vector3(runningJumpHeight * Time.fixedDeltaTime, 0, 0));
        }

        if (Input.GetKey("left shift") && Input.GetKey("space"))
        {
            // jump
            player.AddForce(new Vector3(jumpHeight * Time.fixedDeltaTime, 0, 0));
        }

        if (Input.GetKey("left shift") && Input.GetAxis("Vertical") > 0 && acceleration <= accelerationRateLimit)
        {
            //accelerate
            acceleration += accelerationRate;
        }
        else
        {
            if (acceleration >= 1.5)
            {
                acceleration -= accelerationRate;
            }
            else
            {
                acceleration = 1;
            }
        }
        //*****************************************************

        float ver = Input.GetAxis("Vertical");//getAxis vs getAxisRaw == raw means discreet, not continuous
        float hor = Input.GetAxis("Horizontal");

        //create 3d vector with inputted horizonal and vertical force, vertical also has acceleration
        //Vector3 vec = new Vector3(ver*acceleration * speed, player.velocity.y, hor * speed) * Time.deltaTime; ;//player velocity.y == set y to current y, aka allow for gravity  
       
        vec = new Vector3(Input.GetAxis("Horizontal") * speed, player.velocity.y, Input.GetAxis("Vertical") * speed);
        transform.LookAt(transform.position + new Vector3(vec.x*0.9f, 0f, vec.z*0.9f));
        //player.velocity = vec;

        
    }

    void FixedUpdate()
    {
        player.velocity = vec*acceleration;
    }
    */

}