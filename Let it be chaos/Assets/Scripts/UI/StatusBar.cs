using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Text text;

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
        if(text != null)
        {
            text.text = (Mathf.Round((currentStat / maxStat * 100))).ToString() + "%";
        }
    }
}
