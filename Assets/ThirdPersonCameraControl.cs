using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target, Player, cam;
    private Rigidbody playerBody;
    float mouseX, mouseY;

    //create vector from camera to target
    Vector3 vecDist;
    Vector3 vecDist2;
    //Vector3 worldVec;
    float angle;
    //find difference from created vector to world vector
    //apply difference to movement vector
    Vector3 movement; 
    Vector3 inputVector;
    public float speed;

    float heading = 0;

    void Start()
    {

        playerBody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        CamControl();
        /*
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        cam.rotation = Quaternion.Euler(0, heading, 0);

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        camF = camF * input.y;
        camR = camR* input.x;

        Vector2 camP = camF + camR;

        //movement = new Vector3(camP.x*speed, playerBody.velocity.y, camP.y*speed);
        movement = new Vector3(camP.x*speed, camP.y*speed, playerBody.velocity.y);

        //Debug.Log("Output: (" + movement.x + ", " + movement.y + ", " + movement.z + ") \n");
        playerBody.velocity = movement;

        */
        /*
        Vector2 camVec = new Vector2(cam.position.x - Target.position.x, cam.position.z-Target.position.z);
        Vector2 northVec = new Vector2(cam.position.x - Vector3.forward.x, cam.position.z- Vector3.forward.z);
        northVec = northVec.normalized;
        camVec = camVec.normalized;

        //vecDist = cam2 - Target2.position;//vector from cam to target
        //vecDist2 = cam2.position - Vector3.forward;//vector from cam to forward vector
        //angle = Vector3.Angle(Vector3.forward, vecDist);//angle from new vector to forward relative to world

        //use dot product to find angle between vecDist and vectDist2 (cosTheta = a*b/(||a||*||b||))
        // square each component of a vector, then add up, then square root sum == bottom of fraction aka ||a||*||b||
        //multiply each vector component with each other, then sum all components == top fraction
        // cos theta == fraction

        //angle = Vector2.Dot(new Vector2(vecDist.x, vecDist.z), new Vector2(vecDist2.x, vecDist2.z));
        angle = Vector2.Dot(camVec, northVec);
        Debug.Log("Angle: "+angle);
        inputVector = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        //Debug.Log("Input: (" + inputVector.x + ", " + inputVector.y + ", " + inputVector.z + ") ");
        //movement = Quaternion.Euler(0f, angle, 0f) * inputVector;//rotate movement vector by angle of camera

        //x' = x*costheta(angle) - ysinTheta(angle)
        //y' = x*sinThea(angle) - ycosTheta(angle)
        //float xprime = inputVector.z * Mathf.Cos(angle) - inputVector.x * Mathf.Sin(angle);
        //float zprime = inputVector.z * Mathf.Sin(angle) - inputVector.x * Mathf.Cos(angle);
        //movement = new Vector3(xprime, playerBody.velocity.y, zprime);
        
        inputVector = Quaternion.Euler(0f, angle, 0f) * inputVector;
        movement = new Vector3(inputVector.x, playerBody.velocity.y, inputVector.z);

        //Debug.Log("Output: (" + movement.x + ", " + movement.y + ", " + movement.z + ") \n");
        playerBody.velocity = movement;

        */
        //***************************************************************************************************
        //***************************************************************************************************

        //vSourceToDestination = vDestination - vSource;
        Vector2 camVec = new Vector2(cam.position.x - Target.position.x, cam.position.z - Target.position.z);
        //Vector2 northVec = new Vector2(Vector3.forward.x - cam.position.x, Vector3.forward.z - cam.position.z);
        //northVec = northVec.normalized;//set vector as unit vector, aka magnitude of 1
        camVec = camVec.normalized;

        //use dot product to find angle between vecDist and vectDist2 (cosTheta = a*b/(||a||*||b||))
        // square each component of a vector, then add up, then square root sum == bottom of fraction aka ||a||*||b||
        //multiply each vector component with each other, then sum all components == top fraction
        // cos theta == fraction

        //angle = Vector2.Dot(new Vector2(vecDist.x, vecDist.z), new Vector2(vecDist2.x, vecDist2.z));
        angle = Vector2.Dot(camVec, Vector3.forward);
        Debug.Log("Angle: " + angle);
        inputVector = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        //Debug.Log("Input: (" + inputVector.x + ", " + inputVector.y + ", " + inputVector.z + ") ");
        //movement = Quaternion.Euler(0f, angle, 0f) * inputVector;//rotate movement vector by angle of camera

        //x' = x*costheta(angle) - ysinTheta(angle)
        //y' = x*sinThea(angle) - ycosTheta(angle)
        //float xprime = inputVector.z * Mathf.Cos(angle) - inputVector.x * Mathf.Sin(angle);
        //float zprime = inputVector.z * Mathf.Sin(angle) - inputVector.x * Mathf.Cos(angle);
        //movement = new Vector3(xprime, playerBody.velocity.y, zprime);

        inputVector = Quaternion.Euler(0f, angle, 0f) * inputVector;
        movement = new Vector3(inputVector.x, playerBody.velocity.y, inputVector.z);

        //Debug.Log("Output: (" + movement.x + ", " + movement.y + ", " + movement.z + ") \n");
        playerBody.velocity = movement;
    }
    

    void CamControl()
    {

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            transform.LookAt(Target);
            //transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        }

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
            /*
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            //set player to not lookat if not moving
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                //transform.LookAt(Target);
                transform.LookAt(transform.position+ new Vector3(inputVector.x, 0, inputVector.z));
            }

            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);

            */

    }
    

   
    
    /*
    //*****************************************
    public float rotationSpeed = 1;
    public Transform Player;
    float mouseX, mouseY;
    //*****************************************
    private Rigidbody player;
    private Vector3 input;
    public float speed;
    //*****************************************

    public Camera cam;
    public Transform camTarget;

    //create vector from camera to target
    Vector3 vecDist;
    //Vector3 worldVec;
    float angle;
    //find difference from created vector to world vector
    //apply difference to movement vector
    Vector3 movement;

    void Start()
    {
        //worldVec = new Vector3(1,0,0);
        player = GetComponent<Rigidbody>();
        vecDist = cam.transform.position - camTarget.position;
    }

    void Update()//
    {
        CamControl();
        vecDist = cam.transform.position - camTarget.transform.position;//vector from cam to target
       angle= Vector3.Angle(Vector3.forward, vecDist);//angle from new vector to forward relative to world
       
        movement = Quaternion.Euler(0, -45, 0) * vecDist;//rotate movement vector by angle of camera
    }


    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(camTarget);
    }
    */
        /*
        public float rotationSpeed = 1;
        public Transform Target, Player;
        float mouseX, mouseY;

        public Transform Obstruction;
        public float zoomSpeed = 2f;

        void Start()
        {
            Obstruction = Target;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            CamControl();
            ViewObstructed();
        }


        void CamControl()
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            transform.LookAt(Target);


           // if (Input.GetKey(KeyCode.LeftShift))
           // {
           //     Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
         //   }
           // else
           // {
            //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
           //     Player.rotation = Quaternion.Euler(0, mouseX, 0);
           // }
        }


        void ViewObstructed()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    Obstruction = hit.transform;
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                    if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
                else
                {
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                        transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
        }
    */
    }