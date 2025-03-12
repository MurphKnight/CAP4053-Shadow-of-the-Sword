using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;
     public float maxhealth = 100f;
    public float health;
    public int attack;

    void Start(){
        health = maxhealth;
    }
    void Update(){
        if(healthSlider.value != health){
            healthSlider.value = health;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            takeDamage(10);
        }
    }
    void takeDamage ( float damage){
        health -= damage; 
    }
    public void dealDamage(GameObject target){
        var atm = target.GetComponent<healthBar>();
        if(atm != null){
            atm.takeDamage(attack);
        }
    }
}
