using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] CharacterController2D characterController;

    [SerializeField] int foodNeeded = 5;

    [SerializeField] int currentFood;
    [SerializeField] float staminaRegenModeifier = 0.05f;

    [SerializeField] float maxStamina = 5f;
    float stamina;
    public float jumpStaminaAmount = 2f;
    [SerializeField] float startFlightStaminaAmount = 1f;

    [SerializeField] CircleCollider2D bodyCollider;

    [SerializeField] LayerMask fruits;

    public event Action<int> OnFoodBarUpdate;
    public event Action<float> OnStaminaBarUpdate;

    public event Action GameOverByDeath;

    bool tryInteract = false;
    bool wasInterating = false;

    bool staminaRegen = false;

    void Start()
    {
        ResetValues();
    }

    public void UseStamina(float amount)
    {
        stamina -= amount;
        if (stamina <= 0) stamina = 0;
        OnStaminaBarUpdate?.Invoke(stamina / maxStamina);
    }

    public void ResetValues()
    {
        stamina = maxStamina;
        currentFood = 0;
    }

    void OnItemEaten()
    {
        currentFood++;
        OnFoodBarUpdate?.Invoke(currentFood);
        if (foodNeeded <= currentFood)
        {

            Debug.Log($"<color=red> Full </color>");
        }
    }

    private void Update()
    {
        tryInteract = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
        if (foodNeeded <= currentFood)
            tryInteract = false;


        if (stamina < maxStamina && staminaRegen)
        {
            stamina += Time.deltaTime * staminaRegenModeifier;
            stamina = Mathf.Min(maxStamina, stamina);
            OnStaminaBarUpdate?.Invoke(stamina / maxStamina);
        }
    }
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bodyCollider.transform.position, bodyCollider.radius, fruits);

        if (colliders.Length == 0) return;

        if (tryInteract && characterController.Grounded)
        {
            if (characterController.TryEat(colliders[0].gameObject.GetComponent<FruitObject>()))
                OnItemEaten();
            wasInterating = true;
        }
        else
        {
            if (wasInterating)
            {
                characterController.StopEating();
                wasInterating = false;
            }
        }
    }

    public void StaminaRegenStop()
    {
        staminaRegen = false;
    }

    public void StaminaRegenStart()
    {
        staminaRegen = true;
    }

    public bool CanPerformJump => stamina >= jumpStaminaAmount;
    public bool CanStartFlight => stamina >= startFlightStaminaAmount;
    public bool CanFlight => stamina >= 0.05f;
}
