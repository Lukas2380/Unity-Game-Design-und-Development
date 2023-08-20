using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed1 = 3f;
    public float speed;
    private bool InPause = false;
    public float JumpHeight = 10f;
    public bool CanDash;

    public Transform GroundCheck;
    public LayerMask ground;

    private Rigidbody rb;
    private PlayerMovement m;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HandlePause();

        // Check if the player is grounded
        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
                //jumpSound.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine("CoolDown");
        }
    }

    void FixedUpdate()
    {
        CharacterMovement();
    }

    IEnumerator CoolDown()
    {
        if (CanDash == true)
        {
            CanDash = false;
            rb.AddForce(transform.forward * 6f, ForceMode.Impulse);
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            speed = 0;
        }

        yield return new WaitForSeconds(0.2f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        speed = 6;
        yield return new WaitForSeconds(5);
        CanDash = true;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(GroundCheck.position, 0.5f, ground);
    }

    void CharacterMovement()
    {
        
        // Character Turning with mouse 
        rotation.y += Input.GetAxis("Mouse X");
        transform.eulerAngles = (Vector2)rotation * 3f;
        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveX, 0, moveZ);
        direction.Normalize();
        transform.Translate(direction * (speed * Time.deltaTime));
    }
    
    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (InPause == false)
            {
                InPause = true;
                speed = 0;
                speed1 = 0;
            }
            else
            {
                InPause = false;
                speed = 6;
                speed1 = 3;
            }
        }
    }
}
