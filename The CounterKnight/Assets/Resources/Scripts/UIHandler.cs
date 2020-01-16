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
    
    private int currentUINo = 0;

    // Main UI, just one of them can be active
    private const int ENTRANCE_UI = 0;
    private const int GAME_UI = 1;
    private const int CREDITS_UI = 4;

    // Pop-Up UI, can be active on a main UI
    private const int PAUSE_UI = 2;
    private const int TUTORIAL_UI = 3;
    private const int DEAD_UI = 5;

    private AudioSource aud;

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
        setAudioButtonsImages();
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
        setAudioButtonsImages();
    }

    // EntranceUI Button Functions
    public void startButtonClick()
    {
        setMainUIActivity(GAME_UI);
        GetComponent<Score>().enabled = true;

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

    // AudioSource Button Functions
    private bool musicOn = true;  
    public void setMusicAud(Image buttonImg)
    {
        AudioSource musicAud = aud;

        musicOn = musicOn? false : true;

        musicAud.mute = !musicOn;

        buttonImg.sprite = Resources.Load<Sprite>("UI/Menu/Music" + (musicOn? "On" : "Off") );
    } 

    public static bool soundOn = true;
    public void setSoundAud(Image buttonImg)
    {
        AudioSource[] auds = GameObject.FindObjectsOfType<AudioSource>();

        soundOn = soundOn? false : true;

        buttonImg.sprite = Resources.Load<Sprite>("UI/Menu/Sound" + (soundOn? "On" : "Off") );
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
        EnemySpawnPoint.resetSpawnRate();

        Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        character.resetCharacter();

        DestroyClass.destroyObjectsByTag("Enemy");
        DestroyClass.destroyObjectsByTag("Projectile");
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

    private void setAudioButtonsImages()
    {
        GameObject[] soundAudObjs = GameObject.FindGameObjectsWithTag("Sound");
        GameObject[] musicAudObjs = GameObject.FindGameObjectsWithTag("Music");

        foreach(GameObject soundAudObj in soundAudObjs)
        {
            soundAudObj.GetComponent<Image>().sprite = 
                Resources.Load<Sprite>("UI/Menu/Sound" + (soundOn? "On" : "Off") );
        }

        foreach(GameObject musicAudObj in musicAudObjs)
        {
            musicAudObj.GetComponent<Image>().sprite = 
                Resources.Load<Sprite>("UI/Menu/Music" + (musicOn? "On" : "Off") );
        }
    }
}