using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 10f;
    public float JumpHeight = 10f;
    public bool CanDash;

    public Transform GroundCheck;
    public LayerMask ground;

    private Rigidbody rb;

    public bool isInAir;
    public bool hasDoubleJump;
    public float doubleJumpHeight = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // Check if the player is grounded
            if (IsGrounded())
            {
                isInAir = false;
                hasDoubleJump = true;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
                    //jumpSound.Play();
                    isInAir = true;
                }
            } else if (Input.GetKeyDown(KeyCode.Space) && hasDoubleJump)
            {
                hasDoubleJump = false;
                rb.AddForce(transform.up * doubleJumpHeight, ForceMode.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        CharacterMovement();
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(GroundCheck.position, 1f, ground);
    }

    void CharacterMovement()
    {
        // Character Turning with mouse 
        rotation.y += Input.GetAxis("Mouse X");
        transform.eulerAngles = (Vector2)rotation * 2f;
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveX, 0, moveZ);
        direction.Normalize();
        transform.Translate(direction * (speed * Time.deltaTime));
    }
}