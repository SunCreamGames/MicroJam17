using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] CharacterController2D characterController;

    [SerializeField] int foodNeeded = 5;

    [SerializeField] int currentFood;

    [SerializeField] float stamina;

    [SerializeField] CircleCollider2D bodyCollider;

    [SerializeField] LayerMask fruits;

    public event Action<int> OnFoodBarUpdate;

    public event Action GameOverByDeath;

    bool tryInteract = false;
    bool wasInterating = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        stamina = 5f;
        currentFood = 0;
    }

    void OnItemEaten()
    {
        currentFood++;
        OnFoodBarUpdate?.Invoke(currentFood);
    }

    private void Update()
    {
        tryInteract = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        Debug.Log(tryInteract);
    }
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(bodyCollider.transform.position, bodyCollider.radius, fruits);

        Debug.Log($"<color=cyan>{colliders.Length}</color>");

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
}
