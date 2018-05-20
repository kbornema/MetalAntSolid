using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Image healthBarFilling;
    Vector3 HealthBarPosition;
    Quaternion HealthBarRotation;

    CanvasGroup healthBarCanvas;

    FloatTimer fadeOutTimer;
    public float FadeOutThreshHold;
    public float FadeOutTime;    

    // Use this for initialization
    void Start () {
        Image[] childImages = GetComponentsInChildren<Image>();
        foreach (Image img in childImages)
        {
            if (img.gameObject.tag == "HealthBar")
            {
                healthBarFilling = img;
            }
        }
        if (healthBarFilling == null)
            Debug.Log("no image found");
        HealthBarPosition = this.transform.localPosition;
        HealthBarRotation = this.transform.rotation;

        fadeOutTimer = new FloatTimer(FadeOutTime, false);
        healthBarCanvas = this.GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        fadeOutTimer.Update();
        if(fadeOutTimer.GetDecreasingValue() <= FadeOutThreshHold)
        {
            healthBarCanvas.alpha = (fadeOutTimer.GetDecreasingValue() / FadeOutThreshHold);
        }
    }

    public void LateUpdate()
    {
        this.transform.position = this.transform.parent.transform.position + HealthBarPosition;
        this.transform.rotation = HealthBarRotation;
    }

    public void setHealth(float percent)
    {
        ResetHealthBarFadeOut();
        if (healthBarFilling != null)
        {
            healthBarFilling.fillAmount = percent;
        }
    }

    public void ResetHealthBarFadeOut()
    {
        fadeOutTimer.Reset();
        healthBarCanvas.alpha = 1;
    }
}
