using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MatchManager : MonoBehaviour  {

    [SerializeField]
    private GameCam _mainCamera;
    [SerializeField]
    private ObjectPoolManager _poolManager;
    public ObjectPoolManager PoolManager { get { return _poolManager; } }

    Dictionary<PlayerTyp, int> _resourcesDic;
    Dictionary<PlayerTyp, int> _victoryPointsDic;

    [SerializeField]
    private bool _spawnPlayer = true;
    [SerializeField]
    private int _howOften = 1;
    public Actor playerPrefab;

    #region Singleton
    private static MatchManager _instance = null;
    public static MatchManager Instance()
    {
        return _instance;
    }

    #endregion
    #region ResourcesEvent
    [HideInInspector]
    public class ResourcesEvents : UnityEvent<PlayerTyp, int> { }
    [HideInInspector]
    private ResourcesEvents _resourceEvent = new ResourcesEvents();

    public void InvokeResourceEvent(PlayerTyp playerTyp, int value)
    {
        _resourceEvent.Invoke(playerTyp, value);
    }
    public void AddOnResourceListener(UnityAction<PlayerTyp, int> listener)
    {
        _resourceEvent.AddListener(listener);
    }
# endregion
    #region VictoryEvents
    [HideInInspector]
    public class VictoryEvents : UnityEvent { }
    [HideInInspector]
    private VictoryEvents _victoryEvent = new VictoryEvents();

    public void InvokeVictoryEvent()
    {
        _victoryEvent.Invoke();
    }
    public void AddOnVictoryListener(UnityAction listener)
    {
        _victoryEvent.AddListener(listener);
    }
    #endregion


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        Init();
    }
    private void Init()
    {
        if (_spawnPlayer)
        {
            for (int i = 0; i < _howOften; i++)
                InitPlayer(i);
        }

        _resourcesDic = new Dictionary<PlayerTyp, int>();
        _resourcesDic.Add(PlayerTyp.Player1, 0);
        _resourcesDic.Add(PlayerTyp.Player2, 0);

        _victoryPointsDic = new Dictionary<PlayerTyp, int>();
        _victoryPointsDic.Add(PlayerTyp.Player1, 0);
        _victoryPointsDic.Add(PlayerTyp.Player2, 0);
    }

    public void CalculateResources(PlayerTyp playertyp, int value)
    {
        if (_resourcesDic.ContainsKey(playertyp))
            _resourcesDic[playertyp] += value;

        if (value != 0)
            InvokeResourceEvent(playertyp, _resourcesDic[playertyp]);
    }

    public void InitPlayer(int PlayerID)
    {
        var instance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, null);
        instance.Init(PlayerID);

        if(_mainCamera != null)
            _mainCamera.AddTarget(instance.MyCamTarget);
    }

    public void CalculateVictoryPoints(PlayerTyp playertyp, int value)
    {
        if (_victoryPointsDic.ContainsKey(playertyp))
            _victoryPointsDic[playertyp] += value;

        if (value != 0)
            InvokeVictoryEvent();
    }

    public enum PlayerTyp
    {
        Player1,
        Player2,
        None
    }


    
}
