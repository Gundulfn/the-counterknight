using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [Tooltip("NOTE: Set UI Gameobjects to array like this:\n"   
            +"EntranceUI = 0\n"
            +"GameUI = 1\n"
            +"PauseMenuUI = 2\n"
            +"TutorialUI = 3\n"
            +"CreditsUI = 4")]

    public GameObject[] uiArray = new GameObject[5];
    
    private int currentUINo = 0;

    private const int ENTRANCE_UI = 0;
    private const int GAME_UI = 1;
    private const int PAUSE_UI = 2;
    private const int TUTORIAL_UI = 3;
    private const int CREDITS_UI = 4;

    // GameUI Button Functions
    public void pauseButtonClick()
    {
        Time.timeScale = 0f;
        setUIActivity(PAUSE_UI);
    }
    
    public void resumeButtonClick()
    {
        Time.timeScale = 1f;
        setUIActivity(GAME_UI);
    }

    public void restartButtonClick()
    {
        Score.resetScore();
        EnemySpawnPoint.resetSpawnRate();

        Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        character.resetHp(132426233);
        HpStatus.setHpStatus(character);

        // Resetting Animator Component, search for better way
        character.resetAnimator();

        DestroyClass.destroyObjectsByTag("Enemy");
        DestroyClass.destroyObjectsByTag("Projectile");
        
        Time.timeScale = 1f;
        setUIActivity(GAME_UI);
    }

    public void exitToEntranceButtonClick()
    {
        setUIActivity(ENTRANCE_UI);
    }

    // EntranceUI Button Functions
    public void startButtonClick()
    {
        //Play reborn animation first, after set UI activity
        setUIActivity(GAME_UI);
    }

    public void creditsButtonClick()
    {
        setUIActivity(CREDITS_UI);
    }

    public void exitGameButtonClick()
    {
        Application.Quit();
    }   

    private void setUIActivity(int newUINo)
    {
        currentUINo = newUINo;

        foreach(GameObject ui in uiArray)
        {
            ui.SetActive(false);
        }

        uiArray[currentUINo].SetActive(true);
    }
}
