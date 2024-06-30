using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    LevelButton[] levelButtons;
    [SerializeField]
    LevelButton homeButton;

    private void Start()
    {
        bool allLevelsCompleted = true;
        var levelsCompleted = PlayerPrefs.GetInt(PlayerPrefsVariables.LevelsCompleted);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == levelsCompleted)
            {

                levelButtons[i].Unlock();
                allLevelsCompleted = false;
            }
            else if (i > levelsCompleted)
            {
                allLevelsCompleted = false;
                levelButtons[i].Lock();
            }
            else levelButtons[i].Complete();
        }

        if (allLevelsCompleted)
            homeButton.Unlock();
        else homeButton.Lock();
    }
}
