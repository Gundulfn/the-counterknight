using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public static Skill instance;

    public Image skillImg;
    private Image skillSymbolImg;
    private Button skillButton;
    private Animation skillAnim;

    private const float STACK_LIMIT = 10;
    
    private float stack = 0;
    private string skillName;
    private const string ANIM_CLIP_PATH = "Animations/Skills/";

    void Awake()
    {
        instance = this;
        skillName = PlayerPrefs.GetString("SkillName", "ThunderStrike");

        skillAnim = GetComponent<Animation>();
        skillButton = skillImg.transform.GetChild(0).GetComponent<Button>();
        skillSymbolImg = skillImg.transform.GetChild(1).GetComponent<Image>();
        //Set image according to @param skillName
        resetStack();
    }

    public void resetStack()
    {
        stack = 0;
        skillImg.fillAmount = 0;
        skillSymbolImg.color = new Color(0, 0, 0);

        skillButton.interactable = false;
    }

    public void increaseStack()
    {
        if (stack < STACK_LIMIT) 
        {
            stack += 1;
        } 

        skillImg.fillAmount = stack / 10;
        
        if(stack == 10)
        {
            skillSymbolImg.color = new Color(255, 255, 255);
            skillButton.interactable = true;
        }
        else
        {
            skillSymbolImg.color = new Color(0, 0, 0);
        }
    }

    public void useSkill()
    {
        skillAnim.clip = (AnimationClip)Resources.Load(ANIM_CLIP_PATH + skillName);
        skillAnim.Play();

        resetStack();
    }
}