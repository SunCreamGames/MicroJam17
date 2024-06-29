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
}
