using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float maxHealth = 900f;
    public float currentHealth;
    public Transform player;
    public float attackCooldown;

    private bool phase2 = false;
    private bool phase3 = false;
    private bool canAttack = true;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check health for phase transitions
        if (currentHealth <= maxHealth * 0.7f && !phase2)
        {
            EnterPhase2();
        }
        else if (currentHealth <= maxHealth * 0.3f && !phase3)
        {
            EnterPhase3();
        }

        
        if (canAttack)
        {
            StartCoroutine(AttackCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        canAttack = false;

        int attackType = Random.Range(0, GetMaxAttackType());
        

        if (phase3)
        {
            attackType = GetPhase3AttackType();
        }

        attackCooldown = CooldownForAttack(attackType);
        switch (attackType)
        {
            case 0: ViralSweep(); break;
            case 1: DiseasedDash(); break;
            case 2: CankerousSpit(); break;
            case 3: InfectiousSlam(); break;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    float CooldownForAttack(int attackType)
    {
        switch (attackType)
        {
            case 0: return 6f; 
            case 1: return 7f;  
            case 2: return 5f;  
            case 3: return 10f;  
            default: return 7f;  
        }
    }
    int GetMaxAttackType()
    {
        if (phase3) return 4;  
        if (phase2) return 3;
        else return 2;  
    }
    int GetPhase3AttackType()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue < 40) 
        {
            return 2; 
        }
        else if (randomValue < 75) // 25% chance for Earthshatter
        {
            return 1; // Earthshatter
        }
        else 
        {
            int randomValue2 = Random.Range(0, 100);
            if (randomValue < 50) return 0;
            else return 3;

        }
    }

    void EnterPhase2()
    {
        phase2 = true;
        attackCooldown -= 1f; // Make attacks faster in phase 2
        Debug.Log("Boss has entered Phase 2!");
    }

    void EnterPhase3()
    {
        phase3 = true;
        attackCooldown -= 1f; // Make attacks faster in phase 3
        Debug.Log("Boss has entered Phase 3!");
    }


    void CankerousSpit()
    {
        animator.SetTrigger("CankerousSpit");
        Debug.Log("Attack3");
    }

    void ViralSweep()
    {
        animator.SetTrigger("ViralSweep");
        Debug.Log(" Attack1");
    }

    void DiseasedDash()
    {
        animator.SetTrigger("DiseasedDash");
        Debug.Log(" Attack2 ");
    }

    void InfectiousSlam()
    {
        animator.SetTrigger("InfectiousSlam");
        Debug.Log("Attack4");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSword"))
        {
            TakeDamage(20);

        }
    }

    void Die()
    {
        Debug.Log("Boss Defeated!");
    }
}
