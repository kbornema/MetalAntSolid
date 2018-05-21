using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour  {

    [SerializeField]
    private GameCam _mainCamera;
    [SerializeField]
    private ObjectPoolManager _poolManager;
    public ObjectPoolManager PoolManager { get { return _poolManager; } }

    [SerializeField, Header("Only for Main")]
    private GUI_Fade _fadeCanvas;

    Dictionary<PlayerTyp, int> _resourcesDic;
    Dictionary<PlayerTyp, int> _victoryPointsDic;

    [SerializeField]
    private bool _spawnPlayer = true;
    [SerializeField]
    private int _howOften = 1;
    public Actor playerPrefab;


    List<Actor> _players;

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
        {
            Destroy(gameObject);

            return;
        }


        Init();
    }
    public static void ResetSingletion()
    {
        _instance = null;
    }

    public IEnumerator WaitFunction(float timer, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        yield return new WaitForSeconds(timer);
        obj.gameObject.SetActive(true);
    }


    private void Init()
    {
        if (_fadeCanvas)
            _fadeCanvas.OnClicked(true);

        if (_spawnPlayer)
        {
            _players = new List<Actor>();
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
    public Actor GetOtherPlayer(int myId)
    {
        if (myId == 1)
                return _players[0];
        else
        {
            if (_players.Count > 1)
                return _players[1];
            else
                return null;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainGameOver");
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
        var actor = instance.GetComponent<Actor>();
        _players.Add(actor);

        if (_mainCamera != null)
            _mainCamera.AddTarget(instance.MyCamTarget);
    }

    public void CalculateVictoryPoints(PlayerTyp playertyp, int value)
    {
        if (_victoryPointsDic.ContainsKey(playertyp))
            _victoryPointsDic[playertyp] += value;

        if (value != 0)
            InvokeVictoryEvent();
    }

    public IEnumerator DelayedPlayerRespawn(GameObject player)
    {
        int playerID = player.GetComponent<Actor>().PlayerID;
        ResetPlayer(player);
        player.SetActive(false);
        player.GetComponent<CamTarget>().ValidTarget = false;

        if (!GetOtherPlayer(playerID).gameObject.activeInHierarchy)
            GameOver();

        yield return new WaitForSeconds(3);

        if (!GetOtherPlayer(playerID).gameObject.activeInHierarchy)
            GameOver();

        Debug.Log("Respawning Player " + playerID);
        player.SetActive(true);
        player.GetComponent<CamTarget>().ValidTarget = true;
    }

    public void ResetPlayer(GameObject player)
    {
        // Reset Player Position
        player.transform.position = GetOtherPlayer(player.GetComponent<Actor>().PlayerID).transform.position;
        player.transform.rotation = Quaternion.identity;

        // Reset PlayerScripts
        player.GetComponent<Health>().ResetHP();
        player.GetComponent<Hero_Movement>().antVisual.SetMovePercent(0);
        player.GetComponentInChildren<Hero_Wpn_Controller>().switchToWeapon(playerPrefab.GetComponentInChildren<Hero_Wpn_Controller>().GetComponentInChildren<Hero_Wpn_Info>());
    }


    public enum PlayerTyp
    {
        Player1,
        Player2,
        None
    }


    
}
