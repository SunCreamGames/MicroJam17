using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] CharacterController2D characterController;

    [SerializeField] int foodNeeded = 5;

    [SerializeField] int currentFood;
    [SerializeField] float staminaRegenModeifier = 0.05f;

    [SerializeField] float maxStamina = 5f;
    float stamina;
    public float jumpStaminaAmount = 2f;
    [SerializeField] float startFlightStaminaAmount = 1f;

    [SerializeField] CircleCollider2D bodyCollider;

    [SerializeField] LayerMask fruits, sleepingSpots, sleepingSpotsEntrances;

    [SerializeField] GameObject sleepingSpotEntrance;

    public event Action<int> OnFoodBarUpdate;
    public event Action<float> OnStaminaBarUpdate;

    public event Action GameOverByDeath;
    public event Action OnStomachFull;

    [SerializeField] bool isInCave = false;

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
        var food = PlayerPrefs.GetInt(PlayerPrefsVariables.Food);
        currentFood = food;
        OnFoodBarUpdate?.Invoke(currentFood);
        OnStaminaBarUpdate?.Invoke(stamina / maxStamina);
    }

    void OnItemEaten()
    {
        currentFood++;
        OnFoodBarUpdate?.Invoke(currentFood);
        if (foodNeeded <= currentFood)
        {
            sleepingSpotEntrance.SetActive(true);
        }
    }

    private void Update()
    {
        tryInteract = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


        if (stamina < maxStamina && staminaRegen)
        {
            stamina += Time.deltaTime * staminaRegenModeifier;
            stamina = Mathf.Min(maxStamina, stamina);
            OnStaminaBarUpdate?.Invoke(stamina / maxStamina);
        }
    }
    private void FixedUpdate()
    {
        Collider2D[] fruitColliders = Physics2D.OverlapCircleAll(bodyCollider.transform.position, bodyCollider.radius, fruits);

        if (fruitColliders.Length > 0)
        {
            // фрукти є
            if (tryInteract && characterController.Grounded) // фрути є + жмем шіфт + на землі
            {
                if (characterController.TryEat(fruitColliders[0].gameObject.GetComponent<FruitObject>())) // хаваєм
                    OnItemEaten();
                wasInterating = true;
            }
            else
            {
                if (wasInterating) // фрукти є, але не жмем шіфт/не на землі
                {
                    characterController.StopEating(); // перестаєм хавать
                    wasInterating = false;
                }
            }
        }
        else
        {
            Collider2D[] sleepSpotEntranceColliders = Physics2D.OverlapCircleAll(bodyCollider.transform.position, bodyCollider.radius, sleepingSpotsEntrances);
            if (sleepSpotEntranceColliders.Length > 0)
            {
                if (tryInteract)
                {
                    PlayerPrefs.SetInt(PlayerPrefsVariables.Food, currentFood);
                    if (isInCave)
                        sceneChanger.LoadLevelSceneBack();
                    else
                        sceneChanger.LoadSleepSpotScene();

                }
            }
            else
            {
                Collider2D[] sleepSpotColliders = Physics2D.OverlapCircleAll(bodyCollider.transform.position, bodyCollider.radius, sleepingSpots);
                if (sleepSpotColliders.Length > 0)
                {
                    if (tryInteract && isInCave)
                    {
                        var levelsCompleted = PlayerPrefs.GetInt(PlayerPrefsVariables.LevelsCompleted);
                        levelsCompleted++;
                        PlayerPrefs.SetInt(PlayerPrefsVariables.LevelsCompleted, levelsCompleted);
                        PlayerPrefs.SetInt(PlayerPrefsVariables.Food, 0);
                        sceneChanger.LoadScene("MapScene");
                    }
                }
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
