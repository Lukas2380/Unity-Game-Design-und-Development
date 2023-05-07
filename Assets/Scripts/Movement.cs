using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 rotation = Vector2.zero;
    //speed of the character
    public float speed1 = 3f;
    public float speed;
    //set the game in pause mode
    private bool InPause = false;
    //jumpheight
    public float JumpHeight = 10f;
    //Dashed variables
    public bool CanDash = true;
    public bool Dashed;
    //if the character is grounded
    public bool isGrounded;
    //the cameras in the game
    public Camera cam;
    public Camera cam1;
    // to get rigid body informations
    private Rigidbody rb;
    // creates an object movment
    private Movement m; 

    // gets the script components and the rigidbody components its importand
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m = GetComponent<Movement>();
    }

    // if P key is pressed than set speed t 0
    void Update()
    {
        if (PauseMenu.IsPaused)
            return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (InPause == false)
            {
                InPause = true;
                speed = 0;
                speed1 = 0;
            }
    //is the button pressed again then set the speed of normal stats
            else
            {
                InPause = false;
                speed = 6;
                speed1 = 3;
            }
        }

        // Horizontal = key a and d  to move with * speed * time
        float horizontal_movment = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        // Horizontal = keys w and s  to move with * speed * time
        float vertical_movement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontal_movment, 0, vertical_movement);

        // Character Turning with mouse 
        rotation.y += Input.GetAxis("Mouse X");
        transform.eulerAngles = (Vector2)rotation * speed1;

        //Check if the the character has allready jumped or not 
        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //this for jumping
                rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
            }
        }
        //start the coroutine for the dash cooldown can be triggers by event like button pressed 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            StartCoroutine("CoolDown");
            
        }

    }
    //Coroutine Cooldown
    IEnumerator CoolDown()
    {
        //checks if dash allready used
        if (CanDash == true)
        {
            CanDash = false;
            //Add dashforce forward 
            rb.AddForce(transform.forward * 6f, ForceMode.Impulse);
            // freezeing because the character is not effected by the gravity 0.2f
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            // speed need to be zero because you shouldnt be able to run while dashing 
            speed = 0;
        }
        // freezeing because the character is not effected by the gravity 0.2f
        yield return new WaitForSeconds(0.2f);
        rb.constraints = RigidbodyConstraints.None;
        //the freeze the y position
        rb.constraints = RigidbodyConstraints.FreezeRotationZ| RigidbodyConstraints.FreezeRotationX;
        // after dash activate youre speed
        speed = 6;
        // after dashing start cooldown of 5 seconds
        yield return new WaitForSeconds(5);
      
        CanDash = true;
    }


    //check if the character is grounded 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
