using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRoom : MonoBehaviour
{
    [SerializeField] GameObject cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.SetActive(false);
        }
    }
}
