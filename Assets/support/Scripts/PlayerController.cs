using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private CharacterController characterController;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;
    private Vector3 moveDirection;
    private bool pauseMovement = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check if the "P" key is pressed to pause/unpause movement
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMovement = !pauseMovement;
        }

        // If movement is not paused and the character is grounded, calculate movement direction and apply speed
        if (!pauseMovement && isGrounded)
        {
            // Calculate the movement direction based on input and the character's forward direction
            moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

            // Normalize the movement direction to ensure consistent movement speed
            moveDirection.Normalize();

            // Apply movement speed
            moveDirection *= moveSpeed;

            // Jumping
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            // If movement is paused or not grounded, set the movement direction to zero to stop the character
            moveDirection = Vector3.zero;
        }

        // Apply gravity manually only if the character is not grounded
        if (!isGrounded)
        {
            moveDirection.y -= Time.deltaTime * 9.81f;
        }

        // Move the character using CharacterController
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotate the character based on the horizontal input
        Vector3 rotation = new Vector3(0f, Input.GetAxis("Mouse X"), 0f) * moveSpeed * Time.deltaTime;
        transform.Rotate(rotation);
    }
}
