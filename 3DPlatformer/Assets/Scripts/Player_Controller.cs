using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // ref
    public CharacterController charCont;
    public Transform cam;

    // movement speed vars
    public float speed = 6f; // how fast are we running ?
    public float turnSmoothTime = 0.1f; // value that changes how quickly we turn
    float tSV; // used to not have the player snap
     float runMultiplyer;
    // jump/ grounded vars
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;




    private void Start()
    {
        charCont = GetComponent<CharacterController>(); // grab the players character controller
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform; // grab the players character controller
    }

    // Update is called once per frame
    void Update()
    {
     

        // quickly grab out movemtn vars
        float h = Input.GetAxisRaw("Horizontal"); // grab the horizontal input value
        float v = Input.GetAxisRaw("Vertical"); // grab the vertical input value
        Vector3 dir = new Vector3(h, 0, v).normalized; // set out target direction

        // see if we are moving 
        if (dir.magnitude >= 0.1f)
        {
            // find the  player look angle
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tSV, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // set player look angle

            if (Input.GetButtonDown("Fire1"))
            {
                runMultiplyer = 3.0f;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                runMultiplyer = 0f;
            }


            // move the player
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // make the player move toward camera facing direction
            charCont.Move(moveDir.normalized * (speed + runMultiplyer) * Time.deltaTime); // move the player based on input, speed and of course time
        }


        // check if player is currently grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       //print(isGrounded);



        if(isGrounded && velocity.y <0)
        {
            velocity.y -= 2f;
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           // print("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        // add some gravity
        velocity.y += gravity * Time.deltaTime;
        charCont.Move(velocity * Time.deltaTime);




    }
}
