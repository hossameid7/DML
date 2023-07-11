using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidKill : MonoBehaviour
{
    PauseMenu pauseMenuObject;

    private void Awake()
    {
        pauseMenuObject = FindObjectOfType<PauseMenu>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pauseMenuObject.SetWinOrGameOver(false);
        }
    }
}
