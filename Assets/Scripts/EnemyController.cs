using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 20f;
    public float attackRange; 


    //Adding in all of the audio clips
    public AudioClip[] inswardSlashClips;
    public AudioClip[] outWardSlashClips; 
    public AudioClip[] heavyComboClips;
    private AudioSource audi; 

    float timer;

    //Transform target;
    public Transform player;
    public NavMeshAgent agent;

    private Animator cowboyAnimator;

    float lastAttackStartTime = 0;
    float lastAttackLength = 0;
    float damage;
    bool appliedDamage;
    bool attacking;



    // Start is called before the first frame update
    void Start() {
        // A better way to get player is to create a playerManager script like in Brackeys tutorial
        //player = GameObject.Find("PlayerTempBOX").transform; 
        //agent = GetComponent<NavMeshAgent>();

        timer = 0;
        attackRange = 2.9f;
        damage = 0;
        appliedDamage = false;
        attacking = false;


        // A better way to get player is to create a playerManager script like in Brackeys tutorial
        //player = GameObject.Find("PlayerTempBOX").transform;
        player = GameObject.Find("Magician_RIO_Unity").transform;

        agent = GetComponent<NavMeshAgent>();

        cowboyAnimator = GetComponent<Animator>();

        
        audi = GetComponent<AudioSource>();

        if(audi == null)
        {
            Debug.LogError("Check the audui file");
        }

    }




    // Update is called once per frame
    void Update(){
        // Update the timer
        timer += Time.deltaTime;

        // Get distance from enemy to the player
        float distance = Vector3.Distance(player.position, transform.position);

        //Animator cowboyAnimator = gameObject.GetComponent<Animator>();

        if (distance <= lookRadius) { // start chasing the player
            agent.SetDestination(player.position);


            // FIX THIS WEIRD ERROR where the enemy will walk if its within lookRadius but not running agent.SetDestination(player.position);
            cowboyAnimator.SetBool("isPatroling", true);

            // If the enemy is in range of the player and stops walking towards the player
            // And the player walks around the enemy. The enemy will not be facing the player.
            // This will make the enemy continue facing the player.
            if (distance <= attackRange) // Changed from (distance <= agent.stoppingDistance)
            {
                FaceTarget();
                // Go to back to idle animation 
                cowboyAnimator.SetBool("isPatroling", false);

                // Now start ATTACKING: 
                cowboyAnimator.SetBool("inAttackRange", true);

                // Check if there is already an attack inprogress if not wait
                // Do nothing


                // If there is no attack in-progress start a new attack
                if (Time.time > (lastAttackStartTime + lastAttackLength) ) {
                    //cowboyAnimator.SetBool("inAttackRange", true);
                    Debug.Log("\n\nAttack at " + Time.time);
                    Debug.Log("lastAttackStartTime " + lastAttackStartTime + " and lastAttackLength " + lastAttackLength);
                    attackPlayer(); 
                }
                else // The enemy is within attacking range and is within the attack animation
                {
                    // The enemy is within attack range
                    // and player hasn't parried before the allowed parry time. Apply damage to player
                    // The attack damage to the player hasn't been applied yet
                    if (appliedDamage == false && attacking == true)
                    {
                        appliedDamage = true;

                        // Appy damage to player NOW
                        Debug.Log("Enemy Cowboy attacks at " + Time.time);
                        return;
                    }
                }


            }
            else {  // The Enemy can see the player but is NOT in attack range so set attack range to false
                cowboyAnimator.SetBool("inAttackRange", false);

                // Make sure all attack are off
                cowboyAnimator.SetBool("inwardSlash", false);
                cowboyAnimator.SetBool("outwardSlash", false);
                cowboyAnimator.SetBool("heavyCombo", false);

                attacking = false;
            }
        }
        else {
            //seePlayer = false;
            //cowboyAnimator.SetBool("isPatroling", false); // this breaks animator for some reason
        }
    }

    public void attackPlayer() {
        // The animator should now be in the Attacking StateMachine

        // Make sure all attack are off to be able to choose a new attack
        cowboyAnimator.SetBool("inwardSlash", false);
        cowboyAnimator.SetBool("outwardSlash", false);
        cowboyAnimator.SetBool("heavyCombo", false);

        // Randomly choose an attack 
        float randomNumber = Random.Range(0f, 1f);
        //float damage;
        appliedDamage = false;
        attacking = true;


        if (randomNumber <= 0.37)
        {
            // IF choose the the same attack Exit out and Update chose a new one attack 
            if (lastAttackLength == 2.200f) {
                return;
            }
            cowboyAnimator.SetBool("inwardSlash", true);
            PlayRandomSound(inswardSlashClips);
            lastAttackLength = 2.200f;
            damage = 20;
        }
        else if (randomNumber <= 0.74)
        {
            // IF choose the the same attack Exit out and Update chose a new one attack 
            if (lastAttackLength == 2.033f) {
                return;
            }
            cowboyAnimator.SetBool("outwardSlash", true);
            PlayRandomSound(outWardSlashClips);
            lastAttackLength = 2.033f;
            damage = 25;
        }
        else {
            // IF choose the the same attack Exit out and Update chose a new one attack 
            if (lastAttackLength == 4.667f) {
                return;
            }
            cowboyAnimator.SetBool("heavyCombo", true);
            PlayRandomSound(heavyComboClips);
            lastAttackLength = 4.667f;
            damage = 40;
        }
        // Start the attack timer for this attack 
        lastAttackStartTime = Time.time;
    }


    //this plays a random sound effects from the four clips I implemented 
    //We can change that
    private void PlayRandomSound(AudioClip[] clips)
     {
        if (clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);
            audi.PlayOneShot(clips[randomIndex]);
        }
        else
        {
            Debug.LogWarning("No audio clips assigned for this attack type!");
        }
    }

    void FaceTarget() { 
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(  new Vector3(direction.x, 0, direction.z)  );
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }



    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); // this will visualize the look radius of the enemy in the Unity Editor
    }
}
