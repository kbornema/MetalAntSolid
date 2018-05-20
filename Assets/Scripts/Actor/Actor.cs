using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private LevelSystem _levelSystem;
    [SerializeField]
    EatingFloatingText _eatingFloatingText;

    [SerializeField]
    private CamTarget _camTarget;
    public CamTarget MyCamTarget { get { return _camTarget; } }

    [SerializeField]
    private int _playerID;
    public int PlayerID { get { return _playerID; } }

    #region
    public EatingFloatingText EatingFloating { get { return _eatingFloatingText; } }
    #endregion

    public void Init(int playerId)
    {
        this._playerID = playerId;
    }

    private void Update()
    {

    }
}
