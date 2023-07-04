using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public bool isActive = false;


    [Header("Settings")]
    public Slider VolveAudio;
    public Dropdown DisplayResolistion;
    private Resolution[] its_Resolution;
    public Button TheButtonWindow,ButtonFullScreen;
    public Image TureMark;
    public bool fullScreenMode = true;
    public int NamerModeScreen;

    [Space(15)]
    public int NamberQuallity;
    public Button Qu_Low,QuMedium,QuHight;
    public GameObject QuTrue_Low, QuTrue_Mrdium, QuTrue_Hard;

    [Space(15)]
    public int NamDifficultyLevels;
    public Button DL_Easy, DF_Medium, DF_Hard;
    public GameObject DL_True_Low, DL_True_Mrdium, DL_True_Hard;


    void Start()
    {


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
        FunQuality();
    }

    public void PauseGame(){
        isActive = !isActive;
        Time.timeScale =(isActive == true)? 0: 1;

    }
    public void SellectQuality(int namber)
    {
       NamberQuallity = namber;
       QualitySettings.SetQualityLevel(namber);
    }

    public void SellectDifficultyLevels(int _Sellect)
    {
        NamDifficultyLevels = _Sellect;
    }
     void FunQuality()
    {

        AudioListener.volume =  VolveAudio.value;

        if (fullScreenMode == true)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            TureMark.gameObject.SetActive(true);
            ButtonFullScreen.interactable = false;
            TheButtonWindow.interactable = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            TureMark.gameObject.SetActive(false);
            ButtonFullScreen.interactable = true;
            TheButtonWindow.interactable = false;
        }

        // ــــــــــــــــ تحديد الجودة ــــــــــــــــــــــــــ
        if(NamberQuallity ==0)
        {
            Qu_Low.interactable = false;
            QuMedium.interactable = true;
            QuHight.interactable = true;

            QuTrue_Low.SetActive(true);
            QuTrue_Mrdium.SetActive(false);
            QuTrue_Hard.SetActive(false);

        }
        else if(NamberQuallity == 1)
        {
            Qu_Low.interactable = true;
            QuMedium.interactable = false;
            QuHight.interactable = true;

            QuTrue_Low.SetActive(false);
            QuTrue_Mrdium.SetActive(true);
            QuTrue_Hard.SetActive(false);
        }
        else if (NamberQuallity == 2)
        {
            Qu_Low.interactable = true;
            QuMedium.interactable = true;
            QuHight.interactable = false;

            QuTrue_Low.SetActive(false);
            QuTrue_Mrdium.SetActive(false);
            QuTrue_Hard.SetActive(true);
        }

        // ـــــــــــــــــــ صعوبة اللعب ـــــــــــــــــــــــ
        if (NamDifficultyLevels == 0)
        {
            DL_Easy.interactable = false;
            DF_Medium.interactable = true;
            DF_Hard.interactable = true;

            DL_True_Low.SetActive(true);
            DL_True_Mrdium.SetActive(false);
            DL_True_Hard.SetActive(false);

        }
        else if (NamDifficultyLevels == 1)
        {
            DL_Easy.interactable = true;
            DF_Medium.interactable = false;
            DF_Hard.interactable = true;

            DL_True_Low.SetActive(false);
            DL_True_Mrdium.SetActive(true);
            DL_True_Hard.SetActive(false);

        }
        else if (NamDifficultyLevels == 2)
        {
            DL_Easy.interactable = true;
            DF_Medium.interactable = true;
            DF_Hard.interactable = false;

            DL_True_Low.SetActive(false);
            DL_True_Mrdium.SetActive(false);
            DL_True_Hard.SetActive(true);

        }

    }

    public void Buttons(string itsName)
    {
        if(itsName == "ExitToMenu"){
            SceneManager.LoadScene(0);
        }

    }


    public void ModeScreen(bool Mode)
    {
        fullScreenMode = Mode;
    }

    public void SetResolution(int resolutionIndix)
    {

        Resolution Reso = its_Resolution[resolutionIndix];
        Screen.SetResolution(Reso.width,Reso.height, Screen.fullScreen);

    }
 
    public void DefaultSettings()
    {
        SellectQuality(2);
        SellectDifficultyLevels(0);
        ModeScreen(true);
        VolveAudio.value = 0.5f;

    }
}
