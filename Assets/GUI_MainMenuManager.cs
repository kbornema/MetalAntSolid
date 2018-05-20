using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_MainMenuManager : MonoBehaviour {

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _creditButton;
    [SerializeField]
    private Button _pointlessButton;
    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private GameObject _creditsReference;
    [SerializeField]
    private GameObject _selectMenuReference;

    [SerializeField]
    private Text _pointlessText;
    private int _clickedAmount = 0;

    private void Awake()
    {
        if (_clickedAmount == 0)
            _pointlessText.gameObject.SetActive(false);
        InitButton();
    }

    private void InitButton()
    {
        _startButton.onClick.AddListener(() => StartGame());
        _creditButton.onClick.AddListener(() => Credit());
        _pointlessButton.onClick.AddListener(() => PointlessButtonpressed());
        _exitButton.onClick.AddListener(() => ExitGame());

    }

    private void PointlessButtonpressed()
    {
        _clickedAmount++;
        if (_clickedAmount == 1)
            _pointlessText.gameObject.SetActive(true);
        _pointlessText.text = _clickedAmount.ToString();
    }

    private void StartGame()
    {

    }
    private void Credit()
    {
        _creditsReference.SetActive(true);
        _selectMenuReference.SetActive(false);

    }
    private void ExitGame()
    {

    }
}
