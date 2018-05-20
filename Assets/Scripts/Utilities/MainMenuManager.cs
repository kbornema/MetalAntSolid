using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    private EatingBhvr _startGame;

    private void Awake()
    {
        _startGame.AddOnEatingEventListener(StartGameEating);
    }

    private int _startGameCount;
    private void StartGameEating(int arg0)
    {
        _startGameCount += arg0;
        if(_startGameCount == 100)
        {

        }
    }
}
