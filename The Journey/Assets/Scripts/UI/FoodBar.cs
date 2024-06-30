using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{

    [SerializeField]
    Player player;

    [SerializeField]
    Image[] foodCellFillings;
    [SerializeField]
    Image[] foodCellBgs;

    int currentFoodState;
    void Start()
    {
        currentFoodState = PlayerPrefs.GetInt(PlayerPrefsVariables.Food);
        player.OnFoodBarUpdate += UpdateFoodBar;
        Show();
    }

    private void UpdateFoodBar(int foodAmount)
    {
        currentFoodState = foodAmount;
        for (int i = 0; i < foodCellFillings.Length; i++)
        {
            foodCellFillings[i].enabled = foodAmount > 0;
            foodAmount--;
        }
    }

    public void Hide()
    {
        foreach (var item in foodCellFillings) { item.enabled = false; }
        foreach (var item in foodCellBgs) { item.enabled = false; }
    }
    public void Show()
    {
        foreach (var item in foodCellBgs) { item.enabled = true; }
        UpdateFoodBar(currentFoodState);
    }
}
