using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    public void UpdateBar(float maxStat,float currentStat,bool inverseBar = false)
    {
        if(inverseBar )
        {
            if(currentStat / maxStat >= 1)
            {
                fillImage.fillAmount = 0;
                return;
            }
            fillImage.fillAmount = 1 - currentStat / maxStat;
            return;
        }
        fillImage.fillAmount = (currentStat / maxStat);
    }
}
