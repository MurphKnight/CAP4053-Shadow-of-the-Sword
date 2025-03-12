using System.Collections;
using System.Collections.Generic;
// using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NewerEnemyCowboy : CharacterStats
{

    // From EnemyController
    public float lookRadius = 20f; // How far the enemy can "see"
    public float attackRange; // Stopping distance is 1.6f in Nav Mesh Agent
    //Using string for the randomized attack sound
    // private AudioSource audioSource;
    // public AudioClip[] downwardAttackSounds;
    // public AudioClip[] horizontalAttackSounds;
    // public AudioClip[] comboAttackSounds;

    float timer;
    public Slider healthSlider;

    //Transform target;
    public Transform player;
    public NavMeshAgent agent;

    private Animator cowboyAnimator;
    private string currentState;

    float lastAttackStartTime = 0;
    float lastAttackLength = 0;
    bool appliedDamage;
    bool attacking;

    float lastParriedTime = 0;
    //float lastParriedLength = 0;
    bool playedParry;
    bool tryParry;
    // The length of the parry timing is lastAttackLength/2

    private Renderer weaponRenderer;
    private Material glowMaterial;
    private Material rustMaterial;
    bool glowMaterialOn = false;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        // Saving the player's locations 
        player = GameObject.Find("Magician_RIO_Unity").transform;

        agent = GetComponent<NavMeshAgent>();
        cowboyAnimator = GetComponent<Animator>();

        changeAnimationState("Breathing Idle");

        attackRange = 2.0f; // Stopping distance is 1.6f in Nav Mesh Agent
        appliedDamage = false;
        attacking = false;
        playedParry = false;
        tryParry = false;



        // Get the enemy's weapon and materials
        Transform weaponTransform = transform.Find("root/root.x/spine_01.x/spine_02.x/spine_03.x/shoulder.r/arm_stretch.r/forearm_stretch.r/hand.r/Sword.002"); // "Weapon" is the name of the child object
        weaponRenderer = weaponTransform.GetComponent<Renderer>();

        glowMaterialOn = false;
        glowMaterial = Resources.Load<Material>("Materials/Weapon_Glow");
        rustMaterial = Resources.Load<Material>("Materials/Weapon_Metal_Rust");
    }



    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Get distance from enemy to the player
        float distance = Vector3.Distance(player.position, transform.position);


        // If the enemy is currently playing the death animation
        // the enemy shouldn't be able to change animations
        if (currentState == "Falling Back Death") {
            return;
        }

        
        if (Input.GetMouseButtonDown(1))
        {
            playerTryParry();
        }
        //Update Health
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        // If the parry window is over switch the material back
        if (glowMaterialOn == true && Time.time > (lastAttackStartTime + lastAttackLength/2)) 
        {
            glowMaterialOn = false;
            weaponRenderer.material = rustMaterial;
        }


        // Enemy can "see" the player.
        // So start logic for chasing the player
        if (distance <= lookRadius) {
            // The enemy can only move/walk towards the player if they are not in attack range
            agent.SetDestination(player.position);

            // While walking and attack the enemy should be facing the player
            FaceTarget();

            // Enemy is within attacking distance
            if (distance <= attackRange) {
                if (Time.time > (lastAttackStartTime + lastAttackLength)) // The enemy finished it's last attack
                {
                    //Debug.Log("\n\nAttack at " + Time.time);
                    //Debug.Log("lastAttackStartTime " + lastAttackStartTime + " and lastAttackLength " + lastAttackLength);
                    attackPlayer();

                    
                }
                else // The enemy is within attacking range and is within the attack animation
                {
                    // The enemy is within attack range
                    // and player hasn't parried before the allowed parry time. Apply damage to player
                    // The attack damage to the player hasn't been applied yet
                    if (appliedDamage == false && attacking == true && (lastParriedTime < lastAttackStartTime || lastParriedTime >= (lastAttackStartTime + lastAttackLength / 2) ) )
                    {
                        //tryParry = false;
                        appliedDamage = true;
                        
                        // Appy damage to player NOW
                        Debug.Log("Newer Enemy Cowboy attacks at " + Time.time);
                        return;
                    }

                    // The enemy has just been parried by the player
                    // And the parry hasn't been played before, play the parry animation
                    if (tryParry == true && playedParry == false && lastParriedTime > lastAttackStartTime && lastParriedTime < (lastAttackStartTime + lastAttackLength / 2))
                    {
                        changeAnimationState("Standing React Large From Left");

                        // Update the lastAttackLength to include the reaction time
                        lastAttackLength = 2.267f;
                        playedParry = true;
                    }
                }
            }
            else { // Enemy is NOT within attacking distance so start/continue walk animation 

                // If player has walked away from the enemy and the enemy is still going through an attack.
                // Let the attack finish playing
                if (Time.time < (lastAttackStartTime + lastAttackLength))
                {
                    return;
                }
                changeAnimationState("Walking");
                attacking = false;
            }
        }
        // Enemy can NOT "see" the player.
        else {
            changeAnimationState("Breathing Idle");
        } 
    }




    public void attackPlayer()
    {
        // Randomly choose an attack 
        float randomNumber = Random.Range(0f, 1f);
        appliedDamage = false;
        attacking = true;

        // Here is where the parry window starts
        playedParry = false;
        weaponRenderer.material = glowMaterial;
        glowMaterialOn = true;


        if (randomNumber <= 0.38) // Stable Sword Inward Slash
        {
            cowboyAnimator.Play("Stable Sword Inward Slash");
            currentState = "Stable Sword Inward Slash";
            lastAttackLength = 2.200f;
            //PlayRandomSound(downwardAttackSounds);
            damage = 20;
        }
        else if (randomNumber <= 0.76) // Stable Sword Outward Slash
        {
            cowboyAnimator.Play("Stable Sword Outward Slash");
            currentState = "Stable Sword Outward Slash";
            lastAttackLength = 2.033f;
            //PlayRandomSound(horizontalAttackSounds);
            damage = 20;
        }
        else //(randomNumber <= 0.38) // HeavyCombo
        {
            cowboyAnimator.Play("HeavyCombo");
            currentState = "HeavyCombo";
            lastAttackLength = 4.667f;
            //PlayRandomSound(comboAttackSounds);
            damage = 40;
        }
        // Start the attack timer for this attack 
        lastAttackStartTime = Time.time;
    }


    public void playerTryParry() 
    {
        tryParry = true;
        lastParriedTime = Time.time;
    }



    public override void Die()
    {
        changeAnimationState("Falling Back Death");

        // Maybe add a sound effect

        // Need to fix this later. The seconds shouldn't be hardcoded 
        Destroy(gameObject, 4);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSword"))
        {
            TakeDamage(30);

        }
    }


        void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    // This is mostly for the walk and idle animations.
    // So it doesn't keep replaying these animations
    void changeAnimationState(string newState) {
        if (currentState == newState) return;

        cowboyAnimator.Play(newState);
        currentState = newState;
    }
    private void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);
            //audioSource.PlayOneShot(clips[index]);
        }
    }

    // public void PlayAttackSound(string attackType)
    // {
    //     switch (attackType)
    //     {
    //         case "Downward":
    //             PlayRandomSound(downwardAttackSounds);
    //             break;

    //         case "Horizontal":
    //             PlayRandomSound(horizontalAttackSounds);
    //             break;

    //         case "Combo":
    //             PlayRandomSound(comboAttackSounds);
    //             break;

    //         default:
    //             Debug.LogWarning("Invalid attack type passed to PlayAttackSound!");
    //             break;
    //     }
    // }


}
