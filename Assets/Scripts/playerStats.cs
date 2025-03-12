using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject deathScreen;

    private float currentHealth;
   // public Slider healthBar;

    private void Start(){
        currentHealth = maxHealth;
        UpdateHealthBar();
        deathScreen.SetActive(false);

        //  healthBar.value = maxHealth;
    }

    public void takeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth < 0) currentHealth = 0;
       UpdateHealthBar();
        if(currentHealth == 0)
        {
            // deathScreen.SetActive(true);
            Time.timeScale = 0f;
            SceneManager.LoadScene("DeathScene");
        }
        //healthBar.value = currentHealth;
    }
    private void Update(){


        // healthBar.value = currentHealth;
    }
    private void UpdateHealthBar()
    {
        if (maxHealth > 0)
        {
            healthBar.fillAmount = currentHealth / maxHealth; 
        }
        else
        {
            healthBar.fillAmount = 0;  
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnemySword2"))
        {

            int damageFromEnemy = other.GetComponentInParent<EnemyHollow>().damage;


            takeDamage(damageFromEnemy);

        }
        else if (other.CompareTag("EnemySword1"))
        {
            int damageFromEnemy = other.GetComponentInParent<NewerEnemyCowboy>().damage;
            takeDamage(damageFromEnemy);
        }
        else if (other.CompareTag("EnemySword3")) takeDamage(40);
        else if (other.CompareTag("BossDamage"))
        {

            int damageFromEnemy = other.GetComponentInParent<BossHollow>().damage;


            takeDamage(damageFromEnemy);

        }



    }

}
