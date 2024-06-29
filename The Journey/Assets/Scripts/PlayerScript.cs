using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int hp;
    float food, stamina;
    public event Action GameOverByDeath;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    private void ResetValues()
    {
        hp = 5;
        stamina = 5f;
        food = 5f;
    }

    public void GetDamage(int damageAmount = 1)
    {
        hp -= damageAmount;

        if (hp < 0) hp = 0;

        if (hp == 0)
        {
            GameOverByDeath?.Invoke();
        }

    }
}
