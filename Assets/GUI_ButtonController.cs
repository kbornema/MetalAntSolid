using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_ButtonController : MonoBehaviour {

    public enum GUIMenuTyp
    {
        Game,
        Credits
    }

    [SerializeField]
    private Trash _trash;
    [SerializeField]
    private GUIMenuTyp _typ;

    [SerializeField]
    private GUI_Fade _fade;

    public void Awake()
    {
        _trash.AddOnTrashFinishedEventListener(ButtonPressed);
    }

    private void ButtonPressed()
    {
        switch (_typ)
        {
            case GUIMenuTyp.Game:
                _fade.OnClicked(false, "MainGame");
              
                break;
            case GUIMenuTyp.Credits:

                break;
            default:
                break;
        }

    }
}
