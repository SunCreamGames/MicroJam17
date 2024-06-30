using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    TMP_Text gameOverText;

    [SerializeField]
    Button goBackToMenuButton;

    public void Invoke()
    {
        animator.SetTrigger("DeathScreenInvoke");
    }
}