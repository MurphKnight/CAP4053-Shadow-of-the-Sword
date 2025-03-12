using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationStateManager : MonoBehaviour
{
    Animator animator;
    private bool isWalking;
    private bool isMovementBlocked;
    AudioSource audi;
    private StaminaManager staminaManager;

    private float footstepTimer = 0f; 
    public float footstepInterval = 0.5f;

    public AudioClip footstepClip;

    void Start()
    {
        Debug.Log("ayooooooooo");
        animator = GetComponent<Animator>();
        audi = GetComponent<AudioSource>();    
        Debug.Log(animator);
        isMovementBlocked = false;
        staminaManager = GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {   
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        if (Input.GetMouseButtonDown(0) && !isMovementBlocked && staminaManager.canAct)
        {
            isMovementBlocked = true;
            // animator.SetTrigger("attack");
            animator.SetTrigger("attack");
            StartCoroutine(WaitForAnimation("Attack"));
            
        }
        else if (Input.GetMouseButtonDown(1) && !isMovementBlocked && staminaManager.canAct)
        {
            isMovementBlocked = true;
            animator.SetTrigger("parry");
            StartCoroutine(WaitForAnimation("Parry"));
            
        }
        // If player goes forward        
        else if (!isWalking && forwardPressed && !isMovementBlocked)
        {
            animator.SetBool("isWalking", true);
        }
        else if (isWalking && !forwardPressed && !isMovementBlocked)
        {
            animator.SetBool("isWalking", false);
        }
        if (isWalking)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval && footstepClip != null)
            {
                audi.PlayOneShot(footstepClip); // Play a single footstep sound
                footstepTimer = 0f; // Reset timer
            }
        }
        
    }


    private IEnumerator WaitForAnimation (string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);


        while (stateInfo.IsName("Sword and Shield Slash") && stateInfo.normalizedTime < 5.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
            
        }

        while (stateInfo.IsName("Parry") && stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
            
        }
        // Animation is complete; allow movement again
        isMovementBlocked = false;
    }

    public bool getIsMovementBlocked()
    {
        return isMovementBlocked;
    }
}