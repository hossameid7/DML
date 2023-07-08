using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ithis_MainMenu : MonoBehaviour
{

    public int Level;
    [Header("For Open Any Main in MainMenu")]
    public GameObject[] AllMain;

    public AudioSource AudioButton;

    [Header("Main Liading")]
    public bool BooleanShowLoading;
    public Image FillAmountSmallLoading,BarLevelToOpen;
    public float UseTimeCuli,UseTimeCuliBarLvel;
    public float SppedTurnSmallLoading,SppedBarLevel;

   
    [Header("Main Setting")] 
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

    // Start is called before the first frame update
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
   
    // Update is called once per frame
    void Update()
    {
        if (BooleanShowLoading == true)
        {
            //Small circle download
            UseTimeCuli += Time.deltaTime * SppedTurnSmallLoading;
            FillAmountSmallLoading.fillAmount = UseTimeCuli / 10;
            UseTimeCuli %= 10f;
            //Data loading bar
            UseTimeCuliBarLvel += Time.deltaTime * SppedBarLevel;
            BarLevelToOpen.fillAmount = UseTimeCuliBarLvel / 100;

            if (UseTimeCuliBarLvel > 80f)
            {
                //LoadScene stopped up to 80 percent
                SceneManager.LoadScene(1);
                
                // 
                BooleanShowLoading = false;
            }

        }
        FunSettings();
    }
   
 
    public void OpenButtinMain(string _NameMain)
    {
        //main destination
        if (_NameMain == "_MainLoading")
        {
            BooleanShowLoading = true;
            AllMain[0].SetActive(true);
            AudioButton.Play();
            
        }
        else if(_NameMain == "_MainSetting")
        {
            AllMain[1].SetActive(true);
            AudioButton.Play(); 
        }
        else if(_NameMain == "_MainAbout")
        {
            AllMain[2].SetActive(true);
            AudioButton.Play(); 
        }
        else if(_NameMain == "Exit_Game" )
        {
            Application.Quit();
        } 
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
        ModeScreen(1);
        NamberQuallity = 0;
        namber =0;
        NamberDifficulty = 0;
        Nam_NamberDifficulty =0;
        VolveAudio.value = 0.5f;

       QualitySettings.SetQualityLevel(NamberQuallity);

    }

}