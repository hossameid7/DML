using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public bool isActive = false;

    public GameObject ObjectMainMenu;
    [Header("Settings")]
    public AudioListener _audioListener;
    public Slider VolveAudio;
    public Dropdown DisplayResolistion;
    private Resolution[] its_Resolution;
  
    public bool fullScreenMode = true;
    public int NamerModeScreen;
    
    [Space(5)]
    public int NamberQuallity;
    public Text UIWhatQuality;
    public GameObject NextQuallity,BackQuallity;
    [Space(5)]
    public int NamberDifficulty;
    public GameObject BackDifficulty,NextDifficlty;
    public Text UIWhatDifficulaty;

    [Header("GameOver And Winner")]
    public GameObject AllMainGameOverAndLose;
    public GameObject MainGameOver,MainWinner;

    void Start()
    {


        Time.timeScale = 1;
       its_Resolution = Screen.resolutions;
       DisplayResolistion.ClearOptions();

       List<string> Options = new List<string>();

       int CurrentResilotions = 0;
      
       for (int i = 0; i < its_Resolution.Length; i++)
       {
           string Option = its_Resolution[i].width + " X " + its_Resolution[i].height;
           Options.Add(Option);

           if (its_Resolution[i].width == Screen.currentResolution.width
               && its_Resolution[i].height == Screen.currentResolution.height)
           {
               CurrentResilotions = i;

           }
       }
       
       
       DisplayResolistion.AddOptions(Options);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
            
        }
        FunSettings();
    }

    void FunSettings()
    {


        AudioListener.volume = VolveAudio.value;

        if (fullScreenMode == true)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;         
        }
       
        if(NamberQuallity==0)
        {
            NextQuallity.SetActive(true);
            BackQuallity.SetActive(false);
            UIWhatQuality.text ="Low";

        }
        else if(NamberQuallity==1)
        {
            NextQuallity.SetActive(true);
            BackQuallity.SetActive(true);
            UIWhatQuality.text ="Medium";

        }
        else if(NamberQuallity==2)
        {
             NextQuallity.SetActive(false);
            BackQuallity.SetActive(true);
            UIWhatQuality.text ="Hight";

        }
    
         if(NamberDifficulty==0)
        {
            NextDifficlty.SetActive(true);
            BackDifficulty.SetActive(false);
            UIWhatDifficulaty.text ="Easy";

        }
        else if(NamberDifficulty==1)
        {
            NextDifficlty.SetActive(true);
            BackDifficulty.SetActive(true);
            UIWhatDifficulaty.text ="Medium";

        }
        else if(NamberDifficulty==2)
        {
           NextDifficlty.SetActive(false);
            BackDifficulty.SetActive(true);
            UIWhatDifficulaty.text ="Hard";

        }
    
    }
    public void PauseGame(){
        isActive = !isActive;
        Time.timeScale =(isActive == true)? 0: 1;
        _audioListener.enabled = isActive;
        ObjectMainMenu.SetActive(isActive);
    }
   
    int namber =0;
    public void SellectQuality(bool Next_Back)
    {
        if(Next_Back ==  true)
        {
            namber ++;
        }
        else if(Next_Back ==  false)
        {
            namber --;
        }
       NamberQuallity = namber;
       QualitySettings.SetQualityLevel(NamberQuallity);

    }
    int Nam_NamberDifficulty;
    public void SellectDifficultyLevels(bool Next_Back)
    {
        if(Next_Back ==  true)
        {
            Nam_NamberDifficulty ++;
        }
        else if(Next_Back ==  false)
        {
            Nam_NamberDifficulty --;
        }
        NamberDifficulty = Nam_NamberDifficulty;
    }
    
    
    public void SetWinOrGameOver( bool _What)
    {
        AllMainGameOverAndLose.SetActive(true);
        if(_What == true)
        {
            // Winner
            //SetWinOrGameOver(true)
            MainWinner.SetActive(true);
            MainGameOver.SetActive(false);

        }else
        {
            // GameOver
            //SetWinOrGameOver(false)
            MainGameOver.SetActive(true);
            MainWinner.SetActive(false);
        }
        Time.timeScale =0;
    }

    public void Buttons(string itsName)
    {
        if(itsName == "ExitToMenu"){
            SceneManager.LoadScene(0);
        }else if(itsName == "Return to Menu")
        {
            SceneManager.LoadScene(1);
        }

    }


    public void ModeScreen(int _ModeScreen)
    {
        if(_ModeScreen == 0)
        {
            fullScreenMode  = true;

        }
        else if(_ModeScreen == 1)
        {
            fullScreenMode  = false;

        }
    }

    public void SetResolution(int resolutionIndix)
    {

        Resolution Reso = its_Resolution[resolutionIndix];
        Screen.SetResolution(Reso.width,Reso.height, Screen.fullScreen);

    }
 
    public void DefaultSettings()
    {
       // ModeScreen(0);
        NamberQuallity = 0;
        namber =0;
        NamberDifficulty = 0;
        Nam_NamberDifficulty =0;
        VolveAudio.value = 0.5f;
       QualitySettings.SetQualityLevel(NamberQuallity);

    }
}
