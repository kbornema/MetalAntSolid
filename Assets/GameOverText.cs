using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour {

    [SerializeField]
    private Text _loseText;

    private void Awake()
    {
        _loseText.text
            = "You killed: " + GameManager.Instance().KillValue.ToString()+ " Ants";
        GameManager.Instance().ResetKillValue();
    }
}
