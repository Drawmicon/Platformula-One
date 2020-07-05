using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedsterScript : MonoBehaviour
{
    private Rigidbody rb;      //Reference to Rigidbody Component
    public float speed;        //Speed, updated through script
    public float acceleration; //Every second, the speed will increase by this much
                               //Executes once, when object is spawned / scene loaded
    void Start()
    {
        //Get reference to rigidbody, and set the speed
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.forward * speed;
    }
    //Executes every frame
    void Update()
    {
        //Add acceleration to speed, make sure it's not above topSpeed)
        speed += Time.deltaTime * acceleration;
        //Set object velocity
        rb.velocity = -transform.forward * speed;
    }
}
