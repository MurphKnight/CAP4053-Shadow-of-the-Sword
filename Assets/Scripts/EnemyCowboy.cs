using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCowboy : CharacterStats {
   public Slider healthSlider;

    //float timer;
    public override void Die()
    {
        base.Die();
        
        Animator cowboyAnimator = gameObject.GetComponent<Animator>();
        cowboyAnimator.SetTrigger("isDead");

        // Maybe add a sound effect

        // Errors with this because it keep grabbing the animation length of the state before death, like idle (9 seconds) and walk (1 second)
        float duration = cowboyAnimator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log("Duration of current animation is " + duration);

        Destroy(gameObject, 4); // Need to fix this later. The seconds shouldn't be hardcoded 
        
    }





    public void Attack(CharacterStats playerStats) {
        playerStats.TakeDamage(damage);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PlayerSword"))
        {
            TakeDamage(20);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != currentHealth){
            healthSlider.value = currentHealth;
        }
        
        
       
    }
}
