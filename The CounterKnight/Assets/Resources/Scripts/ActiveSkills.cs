using UnityEngine;
using UnityEngine.UI;

public class ActiveSkills : MonoBehaviour
{
    //ThunderStrike
    public static ActiveSkills instance;
    public Image whitePanel;
    private bool increaseValue = true;

    void Awake()
    {
        instance = this;
    }

    public void thunderStrike()
    {
        if(whitePanel.color.a == 0)
        {
            DestroyClass.destroyObjectsByTag("Enemy");
            Score.increaseScore(DestroyClass.getObjectsCount());
            
            DestroyClass.destroyObjectsByTag("Projectile");
            whitePanel.GetComponent<AudioSource>().Play();
        }
        
        if(whitePanel.color.a == 1)
        {
            increaseValue = false;
        }
        else if(whitePanel.color.a == 0)
        {
            increaseValue = true;
        }

        float aValue = whitePanel.color.a + (increaseValue? 0.25f : -0.25f);
        
        whitePanel.color = new Color(1, 1, 1, aValue);
    }
}
