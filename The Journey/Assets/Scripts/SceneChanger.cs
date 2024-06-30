using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    string sceneToLoad;

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSleepSpotScene()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        var sleepSpotSceneName = currentSceneName + "SleepSpot";
        LoadScene(sleepSpotSceneName);
    }
    public void LoadLevelSceneBack()
    {
        PlayerPrefs.SetInt(PlayerPrefsVariables.IsGettingBackFromCave, 1);
        var currentSceneName = SceneManager.GetActiveScene().name;
        var sleepSpotSceneName = currentSceneName.Replace("SleepSpot", "");
        LoadScene(sleepSpotSceneName);
    }
}