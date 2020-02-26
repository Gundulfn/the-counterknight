using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Tooltip("NOTE: Set UI Gameobjects to array like this:\n"   
            +"EntranceUI = 0\n"
            +"GameUI = 1\n"
            +"PauseMenuUI = 2\n"
            +"TutorialUI = 3\n"
            +"CreditsUI = 4\n"
            +"DeadUI = 5")]

    public GameObject[] uiArray = new GameObject[6];
    public GameObject[] soundAudObjs; //GameObject.FindGameObjectsWithTag("Sound");
    public GameObject[] musicAudObjs; //GameObject.FindGameObjectsWithTag("Music");
    
    private int currentUINo = 0;

    // Main UI, just one of them can be active
    private const int ENTRANCE_UI = 0;
    private const int GAME_UI = 1;
    private const int CREDITS_UI = 4;

    // Pop-Up UI, can be active on a main UI
    private const int PAUSE_UI = 2;
    private const int TUTORIAL_UI = 3;
    private const int DEAD_UI = 5;

    private static AudioSource aud;

    void Awake()
    {
        Time.timeScale = 0;
        aud = GetComponent<AudioSource>();
    }

    // GameUI Button Functions
    public void passTutorialButtonClick()
    {
        uiArray[TUTORIAL_UI].SetActive(false);
        PlayerPrefs.SetInt("OpenTutorial", 0); // Tutorial won't be shown next games
        Time.timeScale = 1;
    }

    public void pauseButtonClick()
    {
        Time.timeScale = 0f;
        uiArray[PAUSE_UI].SetActive(true);
    }
    
    public void resumeButtonClick()
    {
        Time.timeScale = 1f;
        uiArray[PAUSE_UI].SetActive(false);
    }

    public void restartButtonClick()
    {
        resetGame();
        Time.timeScale = 1f;
        setMainUIActivity(GAME_UI);
        aud.Play();
    }

    public void exitToEntranceButtonClick()
    {
        if(uiArray[GAME_UI].activeSelf)
        {
            resetGame();
        }
        
        aud.Stop();
        GetComponent<Score>().enabled = false;
        setMainUIActivity(ENTRANCE_UI);
        
        Time.timeScale = 0;
    }

    // EntranceUI Button Functions
    public void startButtonClick()
    {
        setMainUIActivity(GAME_UI);
        GetComponent<Score>().enabled = true;
        Score.setScoreTexts();

        aud.clip = (AudioClip)Resources.Load("Audios/Musics/GameMusic");
        aud.volume = 0.1f;
        aud.loop = true;
        aud.Play();
        
        if(PlayerPrefs.GetInt("OpenTutorial", 1) == 1)
        {
            uiArray[TUTORIAL_UI].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void creditsButtonClick()
    {
        setMainUIActivity(CREDITS_UI);
    }

    public void exitGameButtonClick()
    {
        Application.Quit();
    }   

    public void setLanguage()
    {
        string lang = PlayerPrefs.GetString("Language", "en");

        if(lang == "en")
        {
            PlayerPrefs.SetString("Language", "tr");
        }
        else
        {
            PlayerPrefs.SetString("Language", "en");
        }

        GetComponent<LanguageOptions>().changeLanguage();
    }

    // AudioSource Button Functions
    private bool musicOn = true;  
    public void setMusicAud(Image buttonImg)
    {
        musicOn = musicOn? false : true;

        aud.mute = !musicOn;

        setAudioButtonsImages(musicAudObjs);
    } 

    public static bool soundOn = true;
    public void setSoundAud(Image buttonImg)
    {
        soundOn = soundOn? false : true;

        setAudioButtonsImages(soundAudObjs);
    }    
    
    // OnPlayerDead UI Function
    public void openDeadUI()
    {
        Time.timeScale = 0;
        uiArray[DEAD_UI].SetActive(true);
        aud.Stop();
    }

    private void resetGame()
    {
        Score.resetScore();
        Skill.instance.resetStack();
        EnemySpawnPoint.resetSpawnPoint();
        Enemy.resetEnemy();
        BossSpawnPoint.resetSpawnPoint();
        Boss.resetBoss();
        Background.resetBackground();

        Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        character.resetCharacter();
        Feet.instance.startAnimation();

        DestroyClass.destroyObjectsByTag("Enemy");
        DestroyClass.destroyObjectsByTag("Projectile");

        aud.clip = (AudioClip)Resources.Load("Audios/Musics/GameMusic");
    }

    // Music AudioClip Function
    private static bool bossMoment = false;

    public static void stopMusicAud()
    {
        aud.Stop();
    }

    public static void changeMusicClip()
    {
        bossMoment = !bossMoment;
        
        if(bossMoment)
        {
            aud.clip = (AudioClip)Resources.Load("Audios/Musics/BossMusic");
        }
        else
        {
            aud.clip = (AudioClip)Resources.Load("Audios/Musics/GameMusic");
        }

        aud.Play();
    }

    // State Checking Functions
    private void setMainUIActivity(int newUINo)
    {
        currentUINo = newUINo;

        foreach(GameObject ui in uiArray)
        {
            ui.SetActive(false);
        }

        uiArray[currentUINo].SetActive(true);
    }

    private void setAudioButtonsImages(GameObject[] audObjs)
    {
        bool audSetting = (audObjs[0].tag == "Music"? musicOn : soundOn);
        string path = "UI/Menu/" + audObjs[0].tag + (audSetting? "On" : "Off");
        
        foreach(GameObject audObj in audObjs)
        {
            audObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
    }
}