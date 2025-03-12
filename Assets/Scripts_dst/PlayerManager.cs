using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float dashSpeed = 13.0f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 1.0f;

    // Drag & Drop the camera in this field, in the inspector
    public Transform cameraTransform;

    private Vector3 moveDirection = Vector3.zero;
    private float dashTime = 0f;
    private bool isDashing = false;
    private bool isMovementBlocked;
    private float lastDashTime = -Mathf.Infinity;

    // Animation
    Animator animator;
    CharacterController controller;
    private StaminaManager staminaManager;
    public Collider swordCollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        staminaManager = GetComponent<StaminaManager>(); // Reference to StaminaManager
        isMovementBlocked = false;
        if (swordCollider == null)
        {
            swordCollider = GameObject.Find("Sword").GetComponent<Collider>(); // Adjust the name to match the actual sword object
        }
        if (swordCollider != null) swordCollider.enabled = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown && !isMovementBlocked && staminaManager.playerStamina >= staminaManager.dashDeduction) // &&staminaManager.canAct)
        {
            StartDash();
            staminaManager.UseStamina(staminaManager.dashDeduction); 
        }

        if (isDashing)
        {
            DashMovement();
        }
        else
        {
            RegularMovement();
        }
        // Update movement
        if (moveDirection != Vector3.zero && moveDirection.magnitude > 1f) // Avoids jittering when there is no movement
        {
            // Create a target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));

            // Smoothly rotate the character
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Adjust rotation speed with 10f
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        HandleSwordCollider();
    }

  

    private void StartDash()
    {
       
        isDashing = true;
        dashTime = 0f;
        lastDashTime = Time.time;
    }

    private void DashMovement()
    {
        // Dash movement is applied here. Move in the direction the player is facing.
        Vector3 dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dashDirection = cameraTransform.TransformDirection(dashDirection);

        // Apply dash speed to movement direction
        moveDirection = dashDirection.normalized * dashSpeed;
        dashTime += Time.deltaTime;

        if (dashTime >= dashDuration)
        {
            isDashing = false;
        }
    }

    private void RegularMovement()
    {
        // Regular movement logic when not dashing
        if (controller.isGrounded)
        {
            Vector3 regularDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            regularDirection = cameraTransform.TransformDirection(regularDirection);
            regularDirection *= speed;  

            if (Input.GetButton("Jump") && staminaManager.playerStamina >= staminaManager.jumpDeduction && staminaManager.canAct)
            {
                regularDirection.y = jumpSpeed;
                staminaManager.UseStamina(staminaManager.jumpDeduction); 
            }

            moveDirection = regularDirection;
        }
    }

private void HandleSwordCollider()
{
    // Check if mouse button 1 (right-click) is pressed
    if (Input.GetMouseButtonDown(0))
    {

        swordCollider.enabled = true;  // Activate the sword collider

        StartCoroutine(DisableColliderAfterDelay(0.73f));

    }
    /*else 
    {
        if (swordCollider != null) swordCollider.enabled = false;
    }*/

}
private IEnumerator DisableColliderAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay); // Wait for the specified duration

    if (swordCollider != null)
    {
        swordCollider.enabled = false; // Disable the sword collider
    }
}

}

//     public float speed = 5.0f;
//     private float horizontalInput;
//     private float verticalInput;

//     // private CharacterController controller;

//     // Start is called before the first frame update
//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         horizontalInput = Input.GetAxis("Horizontal");
//         verticalInput = Input.GetAxis("Vertical");

//         transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
//         transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);


//         // Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput)

//         // float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

//         // // if (Input.GetKey(KeyCode.LeftShift))
//         // // {
//         // //     inputMagnitude *= 2;
//         // // }

//         // float speed = inputMagnitude * maximumSpeed;

//         // Vector3 velocity = movementDirection * speed * Time.deltaTime;

//         // characterController.Move(velocity)

//     }
// }
