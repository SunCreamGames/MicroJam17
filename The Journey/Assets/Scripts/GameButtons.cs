using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour
{
    [SerializeField]
    SceneChanger _sceneChanger;
    public void OnStartGameButtonsClick()
    {
        _sceneChanger.LoadScene("FlightScene");
    }
}
