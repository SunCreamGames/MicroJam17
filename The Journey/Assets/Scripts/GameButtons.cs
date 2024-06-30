using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour
{
    [SerializeField]
    SceneChanger _sceneChanger;
    public void LoadMapScene()
    {
        PlayerPrefs.SetInt(PlayerPrefsVariables.IsGettingBackFromCave, 0);
        _sceneChanger.LoadScene("MapScene");
    }
    public void LoadMenu()
    {
        var soundLevel = PlayerPrefs.GetInt(PlayerPrefsVariables.SoundLevel);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(PlayerPrefsVariables.SoundLevel, soundLevel);
        _sceneChanger.LoadScene("MenuScene");
    }
    public void StartTheGame()
    {
        PlayerPrefs.SetInt(PlayerPrefsVariables.LevelsCompleted, 0);
        PlayerPrefs.SetInt(PlayerPrefsVariables.Food, 0);
        LoadMapScene();
    }
    public void OnLoadDeserLevel()
    {
        _sceneChanger.LoadScene("DesertIslandLevel");
    }
    public void OnLoadJungleLevel()
    {
        _sceneChanger.LoadScene("JungleIslandLevel");
    }

    public void OnLoadWinnigScreen()
    {
        _sceneChanger.LoadScene("WinScene");
    }

}
