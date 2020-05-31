using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    [Header("Character options")]
    public float gravity = 20.0f; 
    public float movementSpeed = 80;
    public float jumpSpeed = 8.0f;

    [Header("Camera options")]
    public Camera cam;
    public float mouseH = 3.0f;
    public float mouseV = 2.0f;
    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    float h_Mouse, v_Mouse;

    private Vector3 move = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        h_Mouse = mouseH * Input.GetAxis("Mouse X");
        v_Mouse += mouseV * Input.GetAxis("Mouse Y");

        v_Mouse = Mathf.Clamp(v_Mouse, minRotation, maxRotation);
        cam.transform.localEulerAngles = new Vector3(-v_Mouse, 0, 0);

        transform.Rotate(0, h_Mouse, 0);


        Movement();
   
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move) * movementSpeed; 

            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
        }
        move.y -= gravity * Time.deltaTime;

        controller.Move(move * Time.deltaTime);
    }
}
