using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for player movement and platform flipping

// reference (character controller tutorial): https://www.youtube.com/watch?v=59No0ybIoxg

public class Player2 : MonoBehaviour
{
    [SerializeField] // used so that variable cannot be changed by other scripts
    private float moveSpeed = 10f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float jumpSpeed = 3.5f;

    private CharacterController controller;

    private float directionY;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { // speed when shift key pressed
            moveSpeed = 20f;
        } else
        {
            moveSpeed = 8f;
        }

        directionY -= gravity * Time.deltaTime;
        direction.y = directionY;
        controller.Move(direction * moveSpeed * Time.deltaTime);
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                directionY = jumpSpeed;
            }
        }
    }
}
