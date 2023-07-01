using UnityEngine;

public class TriggerPauseMenu : MonoBehaviour
{
    public bool isActive = false;
    public GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
    }
    public void PauseGame(){
        isActive = !isActive;
            pauseMenu.SetActive(isActive);
            Time.timeScale =(isActive)? 0: 1;
    }
}
