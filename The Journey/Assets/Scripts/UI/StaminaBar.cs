using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Image barFilling;
    // Start is called before the first frame update
    void Start()
    {
        player.OnStaminaBarUpdate += StaminaBarUpdate;
    }

    private void StaminaBarUpdate(float percentage)
    {
        barFilling.fillAmount = percentage;
    }
}
