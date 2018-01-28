using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


    public void disableBoolInAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", false);
    }

    public void enableBoolInAnimator(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void disableMainInAnimator(Animator anim)
    {
        anim.SetBool("mainMenu", false);
    }

    public void enableMainInAnimator(Animator anim)
    {
        anim.SetBool("mainMenu", true);
    }

    public void disableCreditsInAnimator(Animator anim)
    {
        anim.SetBool("creditsMenu", false);
    }

    public void enableCreditsInAnimator(Animator anim)
    {
        anim.SetBool("creditsMenu", true);
    }

    public void disableSettingslInAnimator(Animator anim)
    {
        anim.SetBool("settingsMenu", false);
    }

    public void enableSettingsInAnimator(Animator anim)
    {
        anim.SetBool("settingsMenu", true);
    }

    public void PRINT()
    {
        print("PRITNIEONT");
    }
}
