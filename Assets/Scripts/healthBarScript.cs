using System.Collections;
using System.Collections.Generic;
// using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
    
    public Slider healthSlider;
     public float maxhealth = 200f;
    public float health;
    public int attack;

    void Start(){
        health = maxhealth;
    }
   
    
    void Update(){
        if(healthSlider.value != health){
            healthSlider.value = health;
        }
    
        if (health <= 0)
        {
            //EnemyCowboy enemyCowboyStatScript = gameObject.GetComponent<EnemyCowboy>();
            //ParentScript parentScript = GetComponentInParent<ParentScript>();
            EnemyCowboy enemyCowboyStatScript = GetComponentInParent<EnemyCowboy>();
            if (enemyCowboyStatScript != null)
            {
                    enemyCowboyStatScript.Die();
            }
            else
            {
                 Debug.Log("EnemyCowboy component not found on this GameObject!");
            }
            NewerEnemyCowboy newerEnemyCowboyStatScript = GetComponentInParent<NewerEnemyCowboy>();
            if (newerEnemyCowboyStatScript != null)
            {
                newerEnemyCowboyStatScript.Die();
            }
            else
            {
                Debug.Log("NewerEnemyCowboy component not found on this GameObject!");
            }
            EnemyHollow enemyHollowStatScript = GetComponentInParent<EnemyHollow>();
            if (enemyHollowStatScript != null)
            {
                enemyHollowStatScript.Die();
            }
            else
            {
                Debug.Log("EnemyHollow component not found on this GameObject!");
            }

        }   
    } 
    public void healthSliderUpdate(GameObject slider){
        if(healthSlider.value != health){
            healthSlider.value = health;
        }
     }

    void takeDamage ( float damage){
        health -= damage; 
       
    }
    
}
