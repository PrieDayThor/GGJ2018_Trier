using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePlayerPanel : MonoBehaviour {

    //true = the player that plays alone
    //false = the other players
    bool gameMode = true;   

    public GameObject onePlayerPanel;
    public GameObject morePlayerPanel;


    public void choosePanel()
    {
        if (gameMode == false)
        {
            onePlayerPanel.SetActive(true);
        }
        else
        {
            morePlayerPanel.SetActive(true);
        }
    }

}
