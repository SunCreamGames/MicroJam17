using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    [SerializeField]
    TMP_Text soundLevelText;

    void Start()
    {
        var soundLevel = PlayerPrefs.GetInt(PlayerPrefsVariables.SoundLevel);
        _slider.value = soundLevel;
        _slider.onValueChanged.AddListener(x =>
        {
            soundLevelText.text = $"Sound : {(int)x}/{_slider.maxValue}";
            PlayerPrefs.SetInt(PlayerPrefsVariables.SoundLevel, (int)x);
        });

        _slider.value = soundLevel;
    }
}
