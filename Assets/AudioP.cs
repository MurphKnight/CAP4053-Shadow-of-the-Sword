using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioP : StateMachineBehaviour
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
    
    private AudioSource audi;

    // This method is called when entering the animation state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the AudioSource component from the Animator's GameObject
        audi = animator.GetComponent<AudioSource>();

        if (audi == null)
        {
            Debug.LogError("AudioSource component not found on the Animator's GameObject!");
            return;
        }

        // Play the footstep sound as the state begins
        audi.Play();
    }

    // Optional: Use this to trigger sounds periodically while in the state
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Example: Play sound at a specific normalized time (e.g., halfway through the animation)
        if (stateInfo.normalizedTime % 1.0f > 0.5f && !audi.isPlaying)
        {
            audi.Play();
        }
    }

    // This method is called when exiting the animation state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop the sound when leaving the state (if needed)
        if (audi != null && audi.isPlaying)
        {
            audi.Stop();
        }
    }
}
