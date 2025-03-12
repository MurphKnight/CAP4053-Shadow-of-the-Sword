using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public float playerStamina = 100.0f;
    private float actionDeduction = 20f;
    private float attackDeduction = 25f;
    public float dashDeduction = 20f; // Deduction for dashing
    public float jumpDeduction = 15f; // Deduction for jumping
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;
    [SerializeField] public bool hasRegenerated = true;
    [Range(0, 50)][SerializeField] private float staminaRegen = 0.5f;
    [SerializeField] public bool areAction;
    [SerializeField] public bool areAttacking;
    private float regenMultiplier = 1f;
    public bool canAct = true;
    private bool runOut = false;

    private PlayerManager playerControler;

    public void Start()
    {
        playerControler = GetComponent<PlayerManager>();

    }
    public void Update()
    {

        if (playerStamina <= 1)
        {
            runOut = true;
            
        }
        if (!areAction && !areAttacking)
        {
            if (playerStamina <= maxStamina)
            {
                playerStamina += regenMultiplier * staminaRegen + Time.deltaTime;
                UpdateStamina();
            }
            if (playerStamina >= maxStamina)
            {
                hasRegenerated = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && canAct)
        {
            AttakDeduction();
        }
        if (Input.GetMouseButtonDown(1) && canAct)
        {
            ActionDeduction();
        }
        RunOut();



    }
    public void RunOut()
    {
        if (runOut)
        {
            if (playerStamina < maxStamina)
            {
                canAct = false;
                regenMultiplier = 1.5f;
            }
            else 
            {
                canAct = true;
                regenMultiplier = 1f; 
                runOut = false;
            }
        }
    }
    public void AttakDeduction()
    {
        UseStamina(attackDeduction);
    }
    public void ActionDeduction()
    {
        UseStamina(actionDeduction);
    }
    public void UseStamina(float amount)
    {

        playerStamina -= amount;
        if (playerStamina < 0) playerStamina = 0;
        UpdateStamina();
    }
    void UpdateStamina()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;
        if (playerStamina == 100.0)
        {
            sliderCanvasGroup.alpha = 0;
        }
        else
        {
            sliderCanvasGroup.alpha = 1;
        }
    }

}
