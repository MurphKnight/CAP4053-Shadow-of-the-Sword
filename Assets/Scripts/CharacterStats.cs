using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {
   //public Slider healthSlider;

    public int test;
    public int maxHealth = 200;

    // Any class can get the currrent health, but only this class can set the health 
    public int currentHealth { get; private set;  }
    
    public int damage;
    public int armor;

    void Awake()
    {
        currentHealth = maxHealth;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage) {
        damage -= armor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}
