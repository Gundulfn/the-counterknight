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

    void Awake()
    {
        Time.timeScale = 0;
    }
    
    void Update()
    {
        if(uiArray[TUTORIAL_UI].activeSelf && Input.touchCount > 0)
        {
            uiArray[TUTORIAL_UI].SetActive(false);
            PlayerPrefs.SetInt("OpenTutorial", 1); // Tutorial won't be shown next games
            Time.timeScale = 1;
        }
    }

    // GameUI Button Functions
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
    }

    public void exitToEntranceButtonClick()
    {
        if(uiArray[GAME_UI].activeSelf)
        {
            resetGame();
        }
        
        GetComponent<Score>().enabled = false;
        setMainUIActivity(ENTRANCE_UI);
    }

    // EntranceUI Button Functions
    public void startButtonClick()
    {
        Time.timeScale = 1;
        //Play reborn animation first, after set UI activity
        setMainUIActivity(GAME_UI);
        GetComponent<Score>().enabled = true;

        if(PlayerPrefs.GetInt("OpenTutorial", 0) == 0 && !Application.isEditor)
        {
            Time.timeScale = 0;
            uiArray[TUTORIAL_UI].SetActive(true);
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
        AudioSource musicAud = GetComponent<AudioSource>();

        musicOn = musicOn? false : true;

        musicAud.mute = !musicOn;

        buttonImg.sprite = Resources.Load<Sprite>("UI/Menu/Music" + (musicOn? "On" : "Off") );
    } 

    private bool soundOn = true;
    public void setSoundAud(Image buttonImg)
    {
        AudioSource[] auds = GameObject.FindObjectsOfType<AudioSource>();

        soundOn = soundOn? false : true;

        foreach(AudioSource aud in auds)
        {
            if(aud.gameObject != this.gameObject)
            {
                aud.mute = !soundOn;       
            }
        }

        buttonImg.sprite = Resources.Load<Sprite>("UI/Menu/Audio" + (soundOn? "On" : "Off") );
    }    
    // OnPlayerDead UI Function
    public void openDeadUI()
    {
        Time.timeScale = 0;
        uiArray[DEAD_UI].SetActive(true);
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

    private void setMainUIActivity(int newUINo)
    {
        currentUINo = newUINo;

        foreach(GameObject ui in uiArray)
        {
            ui.SetActive(false);
        }

        uiArray[currentUINo].SetActive(true);
    }
}
