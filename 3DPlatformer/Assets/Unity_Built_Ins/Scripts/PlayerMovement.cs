using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
    : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController playerController;

    private Vector3 moveDirection;

    


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        

    }

    private void Movement()
    {
        moveDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime);

        playerController.Move(moveDirection * Time.deltaTime);

        
    }

    
}
