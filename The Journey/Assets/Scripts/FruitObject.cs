using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitObject : MonoBehaviour
{
    public float timeToEat;
    float timeLeftToEat;

    [SerializeField]
    Image eatProgressBar;
    private void Awake()
    {
        eatProgressBar.enabled = false;
        eatProgressBar.fillAmount = 0;
        timeLeftToEat = timeToEat;
    }
    public bool InteractWith(float time)
    {
        Debug.Log($"<color=red>Fruit interacting</color>");

        eatProgressBar.enabled = true;
        timeLeftToEat -= time;
        if (timeLeftToEat > 0)
        {
            eatProgressBar.fillAmount = (timeToEat - timeLeftToEat) / timeToEat;
            return false;
        }
        else
        {
            eatProgressBar.enabled = false;
            Destroy(gameObject);
            return true;
        }
    }

    public void StopInteraction()
    {
        eatProgressBar.enabled = false;
    }
}
