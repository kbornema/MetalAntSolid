using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_ButtonController : MonoBehaviour {

    [SerializeField]
    private GameObject _creditAnts;
    [SerializeField]
    private Trash _trash;
    [SerializeField]
    private string _nextScene;
    public enum GUIMenuTyp
    {
        Game,
        Credits
    }
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
                _fade.OnClicked(false, _nextScene);
              
                break;
            case GUIMenuTyp.Credits:
                Instantiate(_creditAnts);
                _trash.Reset();
                MatchManager.Instance().StartCoroutine(MatchManager.Instance().WaitFunction(15, this.gameObject));
                break;
            default:
                break;
        }

    }

}
