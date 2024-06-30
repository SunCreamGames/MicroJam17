using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    Button buttonComponent;
    [SerializeField] Sprite UnlockedSprite;
    [SerializeField] Sprite LockedSprite;
    [SerializeField] Sprite CompletedSprite;

    void Awake()
    {
        buttonComponent = GetComponent<Button>();
    }

    public void Unlock()
    {
        buttonComponent.image.sprite = UnlockedSprite;
        buttonComponent.image.color = Color.white;
        buttonComponent.enabled = true;

    }
    public void Lock()
    {
        buttonComponent.image.sprite = LockedSprite;
        buttonComponent.image.color = Color.black;
        buttonComponent.enabled = false;
    }
    public void Complete()
    {
        buttonComponent.image.sprite = CompletedSprite;
        buttonComponent.image.color = Color.yellow;
        buttonComponent.enabled = false;
    }

}
