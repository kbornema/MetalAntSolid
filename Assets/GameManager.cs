using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    private static GameManager _instance = null;
    public static GameManager Instance()
    {
        return _instance;
    }

    int _killValue = 0;
    public int KillValue { get { return _killValue; } }


    #endregion

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(this);
    }

    public void AddKillValue()
    {
        _killValue++;
    }
    public void ResetKillValue()
    {
        _killValue = 0;
    }
}
